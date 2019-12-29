using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spider.Models;

namespace Spider
{
    public class Spider
    {
        private CookieContainer _cookieContainer;

        public Spider()
        {
            _cookieContainer = new CookieContainer();
        }

        public bool ValidateUrl(string url)
        {
            var result = false;

            try
            {
                var siteUri = new Uri(url);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public ValidationResult ValidateUrlOnSite(string url, string userAgent = "")
        {
            var validationResult = new ValidationResult();
            var uri = new Uri(url);

            var checkUrlManifest = new CheckUrlManifest();
            checkUrlManifest.Url = url;
            checkUrlManifest.UserAgent = userAgent;
            // First we check that the new site domain is working.
            var pageResult = CheckUrl(checkUrlManifest);

            if (pageResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                validationResult.Result = true;
            }
            else
            {
                validationResult.Result = false;
                validationResult.ErrorMessage = $"Validate url: {url} - StatusCode:{pageResult.StatusCode}, expected {System.Net.HttpStatusCode.OK}";
            }

            return validationResult;
        }

        private CheckUrlResult CreateCheckUrlResult(CheckUrlManifest manifest)
        {
            var checkUrlResult = new CheckUrlResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Url = manifest.Url
            };
            return checkUrlResult;
        }

        private HttpWebRequest CreateHttpWebRequest(CheckUrlManifest manifest)
        {
            var webRequest = default(HttpWebRequest);
            //if (ContainPercentEncode(url))
            //{
            //    webRequest = (HttpWebRequest)WebRequest.Create(url);
            //}
            //else
            //{
            var uri = new Uri(manifest.Url);
            webRequest = (HttpWebRequest)WebRequest.Create(uri);

            // Check if we have any cookies that we want to add to the request.
            if (_cookieContainer != null && _cookieContainer.Count != 0)
            {
                webRequest.CookieContainer = _cookieContainer;
            }


            if (manifest.UseProxy)
            {
                var webProxy = new WebProxy(manifest.ProxyAddress);
                webRequest.Proxy = webProxy;
            }

            if (manifest.UseUserAgent)
            {
                webRequest.UserAgent = manifest.UserAgent;
            }

            webRequest.AllowAutoRedirect = false;

            return webRequest;
        }

        private CheckUrlResult HandleWebException(WebException webEx, CheckUrlResult checkUrlResult)
        {
            if (webEx.Message.Contains("404"))
            {
                checkUrlResult.StatusCode = HttpStatusCode.NotFound;
            }
            else if (webEx.Message.Contains("410"))
            {
                checkUrlResult.StatusCode = HttpStatusCode.Gone;
            }
            else if (webEx.Message.Contains("401"))
            {
                checkUrlResult.StatusCode = HttpStatusCode.Unauthorized;
            }
            else if (webEx.Message.Contains("403"))
            {
                checkUrlResult.StatusCode = HttpStatusCode.Forbidden;
            }
            else if (webEx.Message.Contains("408"))
            {
                checkUrlResult.StatusCode = HttpStatusCode.RequestTimeout;
            }
            else if (webEx.Message.Contains("500"))
            {
                checkUrlResult.StatusCode = HttpStatusCode.InternalServerError;
            }
            else if (webEx.Message.Contains("timeout") || webEx.Message.Contains("timed out"))
            {
                //_logger.LogError(webEx, "Operation timeout against the server. Not a regular 408 timeout!");
                checkUrlResult.StatusCode = HttpStatusCode.RequestTimeout;
            }

            return checkUrlResult;
        }

        private CheckUrlResult MakeWebRequest(HttpWebRequest webRequest, CheckUrlManifest manifest, CheckUrlResult checkUrlResult)
        {
            HttpWebResponse webResponse = null;

            try
            {
                // Start timer to check how long time it takes to get response.
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                webResponse = (HttpWebResponse) webRequest.GetResponse();

                checkUrlResult.ContentType = webResponse.ContentType;
                if (webResponse.Cookies.Count != 0)
                {
                    _cookieContainer?.Add(webResponse.Cookies);
                }

                // Only download content for ContentType that are related to HTml, text, xml etc.
                //if (checkUrlResult.ContentType.StartsWith("text/") ||
                //    checkUrlResult.ContentType.StartsWith("application/javascript")
                if(ShouldContentBeDownloaded(checkUrlResult.ContentType, manifest.ContentTypesToDownload))
                //) //TODO: LOW: Detta bör sättas i manifestet så att man kan styra vilka type man vill ladda ned.
                {

                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream != null)
                    {
                        var streamEncoding = Encoding.Default;
                        // Check if we should change the encoding to UTF-8.
                        if (checkUrlResult.ContentType.ToLower().Contains("utf-8"))
                        {
                            streamEncoding = Encoding.UTF8;
                        }

                        using (var reader = new StreamReader(responseStream, streamEncoding))
                        {
                            checkUrlResult.Content = reader.ReadToEnd();
                        }

                        if (checkUrlResult.Size == 0)
                        {
                            checkUrlResult.Size = checkUrlResult.Content.Length;
                        }

                    }
                }

                stopWatch.Stop();
                // Report the response time.
                checkUrlResult.Time = stopWatch.ElapsedMilliseconds;

                checkUrlResult.Size = webResponse.ContentLength;
                checkUrlResult.StatusCode = webResponse.StatusCode;
                checkUrlResult.Server = webResponse.Server;

                // Get Headers information.
                for (int i = 0; i < webResponse.Headers.Count; ++i)
                {
                    checkUrlResult.Headers.Add(webResponse.Headers.Keys[i], webResponse.Headers[i]);
                    checkUrlResult.HeaderData.Add(webResponse.Headers.Keys[i], webResponse.Headers[i]);
                }

                // Get more information from the response.
                checkUrlResult.Redirect = webResponse.Headers["Location"];
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
                checkUrlResult = HandleWebException(webEx, checkUrlResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                checkUrlResult.StatusCode = HttpStatusCode.BadRequest;
                checkUrlResult.Description = $"Could not request {manifest.Url}";
            }

            webResponse?.Close();

            return checkUrlResult;
        }

        private CheckUrlResult CheckRedirect(CheckUrlManifest manifest, CheckUrlResult checkUrlResult)
        {
            //// Get more information from the response.
            //checkUrlResult.Description = webResponse.Headers["Location"];

            var baseUri = new Uri(manifest.Url);
            var locationUri = new Uri(baseUri, checkUrlResult.Redirect);

            var locationUrl = $"{baseUri.Scheme}://{baseUri.Host}{checkUrlResult.Redirect}";

            // Check if we are in a nestled loop
            if (locationUrl == manifest.Url || (manifest.SourceUrls.Any() && manifest.SourceUrls.Contains(locationUri.AbsoluteUri)))
            {
                // Nestled redirection loop.
                checkUrlResult.Erroneous = true;
                checkUrlResult.Description = $"Nestled redirection loop. Redirect to it self ({locationUrl}).";
            }
            else if (locationUri.PathAndQuery.ToLower().Contains("/util/login.aspx"))
            {
                // We have been sent to a EPi server login page. We do not have access to this page.
                checkUrlResult.Erroneous = true;
                checkUrlResult.Description = $"Redirect user to EPi Server login page. ({locationUrl}).";
            }
            else
            {
                // We need to check if the location is a 404 or 410 then we should mark this redirect as erroneous.
                //var locationUri = new Uri(uri, webResponse.Headers["Location"]);
                //uri.AbsoluteUri);
                var newManifest = new CheckUrlManifest
                {
                    Url = locationUrl
                };
                newManifest.SourceUrls = manifest.SourceUrls;
                newManifest.SourceUrls.Add(manifest.Url);
                var locationResult = CheckUrl(newManifest);
                if (locationResult.StatusCode == HttpStatusCode.NotFound || locationResult.StatusCode == HttpStatusCode.Gone ||
                    locationResult.StatusCode == HttpStatusCode.InternalServerError)
                {
                    checkUrlResult.Erroneous = true;
                    checkUrlResult.Description = $"Redirected to {checkUrlResult.Redirect}. Erroneous redirection. Page {checkUrlResult.Redirect} response a 404/410/500 status.";
                }

                // We should also check if the location conatins aspxerrorpath
                if (checkUrlResult.Erroneous == false && checkUrlResult.Redirect.Contains("aspxerrorpath="))
                {
                    // We have been redirected to a ASPX custom error page.
                    checkUrlResult.Erroneous = true;
                    checkUrlResult.Description = $"Redirected to {checkUrlResult.Redirect} ASPX custom error page. Page {checkUrlResult.Redirect} response a soft 500 status.";
                }

                if (locationResult.Erroneous)
                {
                    // Something is wrong but we have not classsified what. Report back the information we got from server.
                    checkUrlResult.Erroneous = true;
                    checkUrlResult.Description = locationResult.Description;
                }
            }

            return checkUrlResult;
        }

        public CheckUrlResult CheckUrl(CheckUrlManifest manifest)
        {
            var checkUrlResult = CreateCheckUrlResult(manifest);

            //HttpWebResponse webResponse = null;
            try
            {
                var webRequest = CreateHttpWebRequest(manifest);

                checkUrlResult = MakeWebRequest(webRequest, manifest, checkUrlResult);
                //// Start timer to check how long time it takes to get response.
                //var stopWatch = new Stopwatch();
                //stopWatch.Start();

                //webResponse = (HttpWebResponse)webRequest.GetResponse();

                //checkUrlResult.ContentType = webResponse.ContentType;
                //if (webResponse.Cookies.Count != 0)
                //{
                //    _cookieContainer?.Add(webResponse.Cookies);
                //}
                //// Only download content for ContentType that are related to HTml, text, xml etc.
                //if (checkUrlResult.ContentType.StartsWith("text/") || checkUrlResult.ContentType.StartsWith("application/javascript")) //TODO: LOW: Detta bör sättas i manifestet så att man kan styra vilka type man vill ladda ned.
                //{

                //    var responseStream = webResponse.GetResponseStream();
                //    if (responseStream != null)
                //    {
                //        var streamEncoding = Encoding.Default;
                //        // Check if we should change the encoding to UTF-8.
                //        if (checkUrlResult.ContentType.ToLower().Contains("utf-8"))
                //        {
                //            streamEncoding = Encoding.UTF8;
                //        }

                //        using (var reader = new StreamReader(responseStream, streamEncoding))
                //        {
                //            checkUrlResult.Content = reader.ReadToEnd();
                //        }

                //        if (checkUrlResult.Size == 0)
                //        {
                //            checkUrlResult.Size = checkUrlResult.Content.Length;
                //        }

                //    }
                //}

                //stopWatch.Stop();
                //// Report the response time.
                //checkUrlResult.Time = stopWatch.ElapsedMilliseconds;

                //checkUrlResult.Size = webResponse.ContentLength;
                //checkUrlResult.StatusCode = webResponse.StatusCode;
                //checkUrlResult.Server = webResponse.Server;

                //// Get Headers information.
                //for (int i = 0; i < webResponse.Headers.Count; ++i)
                //{
                //    checkUrlResult.Headers.Add(webResponse.Headers.Keys[i], webResponse.Headers[i]);
                //}


                // Check if we got activity that we need to lookup more.
                if (checkUrlResult.StatusCode == HttpStatusCode.MovedPermanently || checkUrlResult.StatusCode == HttpStatusCode.Found ||
                    checkUrlResult.StatusCode == HttpStatusCode.TemporaryRedirect)
                {
                    checkUrlResult = CheckRedirect(manifest, checkUrlResult);
                    ////// Get more information from the response.
                    ////checkUrlResult.Description = webResponse.Headers["Location"];

                    //var baseUri = new Uri(manifest.Url);
                    //var locationUri = new Uri(baseUri, checkUrlResult.Redirect);

                    //var locationUrl = $"{baseUri.Scheme}://{baseUri.Host}{checkUrlResult.Redirect}";

                    //// Check if we are in a nestled loop
                    //if (locationUrl == manifest.Url || (manifest.SourceUrls.Any() && manifest.SourceUrls.Contains(locationUri.AbsoluteUri)))
                    //{
                    //    // Nestled redirection loop.
                    //    checkUrlResult.Erroneous = true;
                    //    checkUrlResult.Description = $"Nestled redirection loop. Redirect to it self ({locationUrl}).";
                    //}
                    //else if (locationUri.PathAndQuery.ToLower().Contains("/util/login.aspx"))
                    //{
                    //    // We have been sent to a EPi server login page. We do not have access to this page.
                    //    checkUrlResult.Erroneous = true;
                    //    checkUrlResult.Description = $"Redirect user to EPi Server login page. ({locationUrl}).";
                    //}
                    //else
                    //{
                    //    // We need to check if the location is a 404 or 410 then we should mark this redirect as erroneous.
                    //    //var locationUri = new Uri(uri, webResponse.Headers["Location"]);
                    //     //uri.AbsoluteUri);
                    //    var newManifest = new CheckUrlManifest
                    //    {
                    //        Url = locationUrl
                    //    };
                    //    newManifest.SourceUrls = manifest.SourceUrls;
                    //    manifest.SourceUrls.Add(manifest.Url);
                    //    var locationResult = CheckUrl(newManifest);
                    //    if (locationResult.StatusCode == HttpStatusCode.NotFound || locationResult.StatusCode == HttpStatusCode.Gone ||
                    //        locationResult.StatusCode == HttpStatusCode.InternalServerError)
                    //    {
                    //        checkUrlResult.Erroneous = true;
                    //        checkUrlResult.Description = $"Redirected to {checkUrlResult.Redirect}. Erroneous redirection. Page {checkUrlResult.Redirect} response a 404/410/500 status.";
                    //    }

                    //    // We should also check if the location conatins aspxerrorpath
                    //    if (checkUrlResult.Erroneous == false && checkUrlResult.Description.Contains("aspxerrorpath="))
                    //    {
                    //        // We have been redirected to a ASPX custom error page.
                    //        checkUrlResult.Erroneous = true;
                    //        checkUrlResult.Description = $"Redirected to {checkUrlResult.Redirect} ASPX custom error page. Page {checkUrlResult.Redirect} response a soft 500 status.";
                    //    }

                    //    if (locationResult.Erroneous)
                    //    {
                    //        // Something is wrong but we have not classsified what. Report back the information we got from server.
                    //        checkUrlResult.Erroneous = true;
                    //        checkUrlResult.Description = locationResult.Description;
                    //    }
                    //}
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
                checkUrlResult = HandleWebException(webEx, checkUrlResult);
            }
            catch (Exception catchEx)
            {
                Console.WriteLine(catchEx.Message);
                checkUrlResult.StatusCode = HttpStatusCode.BadRequest;
                checkUrlResult.Description = $"Could not request {manifest.Url}";
            }

            

            return checkUrlResult;
        }

        public bool ShouldUrlBeIgnored(string url, List<string> listOfRegExPatterns)
        {
            var shouldBeIgnored = false;
            foreach (var pattern in listOfRegExPatterns)
            {
                if (Regex.IsMatch(url, pattern))
                {
                    shouldBeIgnored = true;
                    break;
                }
            }

            return shouldBeIgnored;
        }

        public bool ShouldContentBeDownloaded(string contentType, List<string> listOfAllowedContentTypes)
        {
            var beDownloaded = false;
            foreach (var pattern in listOfAllowedContentTypes)
            {
                if (contentType.StartsWith(pattern))
                {
                    beDownloaded = true;
                    break;
                }
            }

            return beDownloaded;
        }

        //public CheckUrlResult CheckUrl(string url, List<string> sourceUrls, string userAgent = "", string proxyAddress = "")
        //{
        //    var result = new CheckUrlResult
        //    {
        //        StatusCode = HttpStatusCode.BadRequest,
        //        Url = url
        //    };

        //    //if (uri.AbsoluteUri == "https://www.rule.sehttps//www.rule.se//wp-login.php?action=logout&_wpnonce=839bbce889")
        //    //{
        //    //    Console.WriteLine("Start debug.");
        //    //}

        //    HttpWebResponse webResponse = null;
        //    try
        //    {
        //        //var webRequest = (HttpWebRequest)WebRequest.Create(uri);
        //        var webRequest = default(HttpWebRequest);
        //        //if (ContainPercentEncode(url))
        //        //{
        //        //    webRequest = (HttpWebRequest)WebRequest.Create(url);
        //        //}
        //        //else
        //        //{
        //        var uri = new Uri(url);
        //        webRequest = (HttpWebRequest)WebRequest.Create(uri);

        //        // Check if we have any cookies that we want to add to the request.
        //        if (_cookieContainer != null && _cookieContainer.Count != 0)
        //        {
        //            webRequest.CookieContainer = _cookieContainer;
        //        }


        //        if (!string.IsNullOrEmpty(proxyAddress))
        //        {
        //            var webProxy = new WebProxy(proxyAddress);
        //            webRequest.Proxy = webProxy;
        //        }

        //        //}

        //        if (!string.IsNullOrEmpty(userAgent))
        //        {
        //            webRequest.UserAgent = userAgent;
        //        }

        //        webRequest.AllowAutoRedirect = false;

        //        // Start timer to check how long time it takes to get response.
        //        var stopWatch = new Stopwatch();
        //        stopWatch.Start();

        //        webResponse = (HttpWebResponse)webRequest.GetResponse();
        //        result.ContentType = webResponse.ContentType;
        //        if (webResponse.Cookies.Count != 0)
        //        {
        //            _cookieContainer?.Add(webResponse.Cookies);
        //        }
        //        // Only download content for ContentType that are related to HTml, text, xml etc.
        //        if (result.ContentType.StartsWith("text/") || result.ContentType.StartsWith("application/javascript")) //TODO: LOW: Detta bör sättas i manifestet så att man kan styra vilka type man vill ladda ned.
        //        {

        //            var responseStream = webResponse.GetResponseStream();
        //            if (responseStream != null)
        //            {
        //                var streamEncoding = Encoding.Default;
        //                // Check if we should change the encoding to UTF-8.
        //                if (result.ContentType.ToLower().Contains("utf-8"))
        //                {
        //                    streamEncoding = Encoding.UTF8;
        //                }

        //                using (var reader = new StreamReader(responseStream, streamEncoding))
        //                {
        //                    result.Content = reader.ReadToEnd();
        //                }

        //                if (result.Size == 0)
        //                {
        //                    result.Size = result.Content.Length;
        //                }

        //            }
        //        }

        //        stopWatch.Stop();
        //        // Report the response time.
        //        result.Time = stopWatch.ElapsedMilliseconds;

        //        result.Size = webResponse.ContentLength;
        //        result.StatusCode = webResponse.StatusCode;
        //        result.Server = webResponse.Server;

        //        // Get Headers information.
        //        for (int i = 0; i < webResponse.Headers.Count; ++i)
        //        {
        //            result.Headers.Add(webResponse.Headers.Keys[i], webResponse.Headers[i]);
        //        }


        //        // Check if we got activity that we need to lookup more.
        //        if (result.StatusCode == HttpStatusCode.MovedPermanently || result.StatusCode == HttpStatusCode.Found ||
        //            result.StatusCode == HttpStatusCode.TemporaryRedirect)
        //        {
        //            // Get more information from the response.
        //            result.Description = webResponse.Headers["Location"];

        //            var baseUri = new Uri(url);
        //            var locationUri = new Uri(baseUri, webResponse.Headers["Location"]);

        //            var locationUrl = $"{baseUri.Scheme}://{baseUri.Host}{webResponse.Headers["Location"]}";

        //            // Check if we are in a nestled loop
        //            if (locationUrl == url || (sourceUrls.Any() && sourceUrls.Contains(locationUri.AbsoluteUri)))
        //            {
        //                // Nestled redirection loop.
        //                result.Erroneous = true;
        //                result.Description = $"Nestled redirection loop. Redirect to it self ({locationUrl}).";
        //            }
        //            else if (locationUri.PathAndQuery.ToLower().Contains("/util/login.aspx"))
        //            {
        //                // We have been sent to a EPi server login page. We do not have access to this page.
        //                result.Erroneous = true;
        //                result.Description = $"Redirect user to EPi Server login page. ({locationUrl}).";
        //            }
        //            else
        //            {
        //                // We need to check if the location is a 404 or 410 then we should mark this redirect as erroneous.
        //                //var locationUri = new Uri(uri, webResponse.Headers["Location"]);
        //                sourceUrls.Add(url); //uri.AbsoluteUri);
        //                var locationResult = CheckUrl(locationUrl, sourceUrls);
        //                if (locationResult.StatusCode == HttpStatusCode.NotFound || locationResult.StatusCode == HttpStatusCode.Gone ||
        //                    locationResult.StatusCode == HttpStatusCode.InternalServerError)
        //                {
        //                    result.Erroneous = true;
        //                    result.Description = $"Redirected to {webResponse.Headers["Location"]}. Erroneous redirection. Page {webResponse.Headers["Location"]} response a 404/410/500 status.";
        //                }

        //                // We should also check if the location conatins aspxerrorpath
        //                if (result.Erroneous == false && result.Description.Contains("aspxerrorpath="))
        //                {
        //                    // We have been redirected to a ASPX custom error page.
        //                    result.Erroneous = true;
        //                    result.Description = $"Redirected to {webResponse.Headers["Location"]} ASPX custom error page. Page {webResponse.Headers["Location"]} response a soft 500 status.";
        //                }

        //                if (locationResult.Erroneous)
        //                {
        //                    // Something is wrong but we have not classsified what. Report back the information we got from server.
        //                    result.Erroneous = true;
        //                    result.Description = locationResult.Description;
        //                }
        //            }
        //        }
        //    }
        //    catch (WebException webEx)
        //    {
        //        if (webEx.Message.Contains("404"))
        //        {
        //            result.StatusCode = HttpStatusCode.NotFound;
        //        }
        //        else if (webEx.Message.Contains("410"))
        //        {
        //            result.StatusCode = HttpStatusCode.Gone;
        //        }
        //        else if (webEx.Message.Contains("401"))
        //        {
        //            result.StatusCode = HttpStatusCode.Unauthorized;
        //        }
        //        else if (webEx.Message.Contains("403"))
        //        {
        //            result.StatusCode = HttpStatusCode.Forbidden;
        //        }
        //        else if (webEx.Message.Contains("408"))
        //        {
        //            result.StatusCode = HttpStatusCode.RequestTimeout;
        //        }
        //        else if (webEx.Message.Contains("500"))
        //        {
        //            result.StatusCode = HttpStatusCode.InternalServerError;
        //        }
        //        else if (webEx.Message.Contains("timeout") || webEx.Message.Contains("timed out"))
        //        {
        //            //_logger.LogError(webEx, "Operation timeout against the server. Not a regular 408 timeout!");
        //            result.StatusCode = HttpStatusCode.RequestTimeout;
        //        }
        //    }
        //    catch (Exception catchEx)
        //    {
        //        Console.WriteLine(catchEx.Message);
        //        result.StatusCode = HttpStatusCode.BadRequest;
        //        result.Description = $"Could not request {url}";
        //        //Biz.Log.LogWarning($"Could not request {url}: {catchEx.Message}");
        //    }

        //    webResponse?.Close();

        //    return result;
        //}

        public dynamic GetExternalData(string url, string proxyAddress = "")
        {
            dynamic dynamicResult;
            using (var wc = new WebClient())
            {
                if (!string.IsNullOrEmpty(proxyAddress))
                {
                    var webProxy = new WebProxy(proxyAddress);
                    wc.Proxy = webProxy;
                }

                var uri = new Uri(url);

                var result = wc.DownloadString(uri);

                dynamicResult = ConvertToDynamic(result);
            }

            return dynamicResult;
        }

        public dynamic ConvertToDynamic(string data)
        {
            dynamic json = JValue.Parse(data);
            return json;
        }


        ///// <summary>
        ///// Check if a URL contain percent-encode for special chars.
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //public bool ContainPercentEncode(string url)
        //{
        //    var containPercentEncode = false;

        //    try
        //    {
        //        var uri = new Uri(url);
        //        if (uri.AbsoluteUri != url)
        //        {
        //            containPercentEncode = true;
        //        }
        //    }
        //    catch { }

        //    return containPercentEncode;
        //}
    }
}
