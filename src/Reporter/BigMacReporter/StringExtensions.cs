using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter
{
    public static class StringExtensions
    {
        public static string ToStringTrim(this StringBuilder sb)
        {
            return sb.ToString().TrimWhiteSpace();
        }

        public static string TrimWhiteSpace(this string str)
        {
            string tbr = str;
            while (tbr.Contains("  ")) tbr = tbr.Replace("  ", " ");

            return tbr.Trim();
        }
    }
}
