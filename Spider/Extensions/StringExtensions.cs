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

    }
}
