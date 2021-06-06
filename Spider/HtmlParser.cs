using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spider
{
    public static class HtmlParser
    {
        private const string _anchorRegexPattern = "<a[^>]*href=\"([^\"]*)\"[^>]*>"; //"<a[^>]*href=\"([^\"]*)\"[^>]*>.*<\\/a>";

        public static List<string> GetAllAnchors(string content, string anchorRegexPattern = "")
        {
            var linkList = new List<string>();
            if (string.IsNullOrEmpty(anchorRegexPattern))
            {
                anchorRegexPattern = _anchorRegexPattern;
            }

            if (!string.IsNullOrEmpty(content))
            {
                var matches = Regex.Matches(content, anchorRegexPattern, RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    var link = match.Groups[1].ToString();

                    linkList.Add(link);
                }
            }

            return linkList;
        }

        private const string _iframeRegexPattern = "<iframe.*?src=\"(.*?)\".*?";
        public static List<string> GetAllIframes(string content, string iframeRegexPattern = "")
        {
	        var linkList = new List<string>();
	        if (string.IsNullOrEmpty(iframeRegexPattern))
	        {
		        iframeRegexPattern = _iframeRegexPattern;
	        }

	        if (!string.IsNullOrEmpty(content))
	        {
		        var matches = Regex.Matches(content, iframeRegexPattern, RegexOptions.IgnoreCase);

		        foreach (Match match in matches)
		        {
			        var link = match.Groups[1].ToString();

			        linkList.Add(link);
		        }
	        }

	        return linkList;

        }

        private const string _scriptRegexPattern = "<script.*?src=\"(.*?)\".*?>";
        public static List<string> GetAllScripts(string content, string scriptRegexPattern = "")
        {
	        var linkList = new List<string>();
	        if (string.IsNullOrEmpty(scriptRegexPattern))
	        {
		        scriptRegexPattern = _scriptRegexPattern;
	        }

	        if (!string.IsNullOrEmpty(content))
	        {
		        var matches = Regex.Matches(content, scriptRegexPattern, RegexOptions.IgnoreCase);

		        foreach (Match match in matches)
		        {
			        var link = match.Groups[1].ToString();

			        linkList.Add(link);
		        }
	        }

	        return linkList;

        }

    }
}
