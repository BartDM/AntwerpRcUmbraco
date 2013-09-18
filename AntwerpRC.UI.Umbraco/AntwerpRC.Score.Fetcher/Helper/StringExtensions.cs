using System.Text.RegularExpressions;

namespace AntwerpRC.Score.Fetcher.Helper
{
    public static class StringExtensions
    {
        public static string CleanHtml(this string str)
        {
            str = Regex.Replace(str, @"(&nbsp;)", "");
            return str;
        }
    }
}
