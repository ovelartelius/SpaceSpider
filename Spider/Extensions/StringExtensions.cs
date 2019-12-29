using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spider.Extensions
{
    public static class StringExtensions
    {
        public static string ToFileSafeName(this string uri)
        {
            var safeString = Regex.Replace(uri, @"[^0-9a-zA-Z]+", "");

            return safeString;
        }

        public static List<string> SplitToList(this string data)
        {
            var list = new List<string>();
            if (!string.IsNullOrEmpty(data))
            {
                var patterns = data.Split('\n');
                foreach (var pattern in patterns)
                {
                    var patternValue = pattern;
                    if (patternValue.Contains("\r"))
                    {
                        patternValue = patternValue.Replace("\r", "");
                    }
                    list.Add(patternValue);
                }
            }

            return list;
        }

        public static string SwapHostname(this string oldUrl, string newHostname)
        {
            var oldUri = new Uri(oldUrl);
            var newHostnameUri = new Uri(newHostname);
            var newUrl = string.Empty;
            if (newHostnameUri.Port != 80 && newHostnameUri.Port != 443)
            {
                newUrl = $"{newHostnameUri.Scheme}://{newHostnameUri.Host}:{newHostnameUri.Port}{oldUri.PathAndQuery}";
            }
            else
            {
                newUrl = $"{newHostnameUri.Scheme}://{newHostnameUri.Host}{oldUri.PathAndQuery}";
            }

            return newUrl;
        }

        public static bool MatchAnyPattern(this string checkIfMatch, List<string> patterns)
        {
            var isMatch = false;

            if (patterns != null)
            {
                foreach (var pattern in patterns)
                {
                    if (Regex.IsMatch(checkIfMatch, pattern))
                    {
                        Console.WriteLine($"Ignore URL {checkIfMatch} for pattern {pattern}");
                        isMatch = true;
                        break;
                    }
                }
            }

            return isMatch;
        }
    }
}
