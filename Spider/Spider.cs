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
	            Console.WriteLine(webRequest.Address);
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
            Uri locationUri = null;
            var locationUrl = string.Empty;

            if (ValidateUrl(checkUrlResult.Redirect))
            {
				locationUri = new Uri(checkUrlResult.Redirect);
				locationUrl = checkUrlResult.Redirect;
            }
            else
            {
	            var baseUri = new Uri(manifest.Url);
	            locationUri = new Uri(baseUri, checkUrlResult.Redirect);

	            locationUrl = $"{baseUri.Scheme}://{baseUri.Host}{checkUrlResult.Redirect}";

            }


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

        public PsCheckUrlResult PsCheckUrl(string url, string userAgent = "")
        {
	        var valid = ValidateUrl(url);

            var manifest = new CheckUrlManifest
	        {
		        Url = url,
		        UserAgent = userAgent
	        };

            var result = CreateCheckUrlResult(manifest);

            if (valid)
            {
	            result = CheckUrl(manifest);
            }
            else
            {
	            result.Erroneous = true;
            }

            var checkUrlResult = new PsCheckUrlResult
	        {
		        Content = result.Content,
		        Url = result.Url,
		        StatusCode = ((int)result.StatusCode),
		        Description = result.Description,
		        Redirect = result.Redirect,
		        Erroneous = result.Erroneous,
		        Time = result.Time,
		        Size = result.Size,
		        ContentType = result.ContentType,
		        Server = result.Server,
		        HistoricHits = result.HistoricHits,
		        Ignored = result.Ignored,
		        IgnoredLinks = result.IgnoredLinks,
		        ErroneousLinks = result.ErroneousLinks,
		        DestinationUrls = result.DestinationUrls,
		        ExternalUrls = result.ExternalUrls,
                IsRobotsTxt = result.IsRobotsTxt,
                IsSitemapXml = result.IsSitemapXml,
                IsSiteDomain = result.IsSiteDomain,
                HasEpiserverLicenseProblem = result.HasEpiserverLicenseProblem
            };

	        return checkUrlResult;
        }

        public CheckUrlResult CheckUrl(CheckUrlManifest manifest)
        {
            var checkUrlResult = CreateCheckUrlResult(manifest);

            try
            {
                var webRequest = CreateHttpWebRequest(manifest);

                checkUrlResult = MakeWebRequest(webRequest, manifest, checkUrlResult);

                // Check if we got activity that we need to lookup more.
                if (checkUrlResult.StatusCode == HttpStatusCode.MovedPermanently || checkUrlResult.StatusCode == HttpStatusCode.Found ||
                    checkUrlResult.StatusCode == HttpStatusCode.TemporaryRedirect)
                {
                    checkUrlResult = CheckRedirect(manifest, checkUrlResult);
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

    }
}
