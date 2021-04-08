using System;
using System.Text;

namespace Craftz.Core
{
    public static class StringHelpers
    {
        // There seems to be a limit for EscapeDataString for strings longer than 32766 characters
        // https://docs.microsoft.com/en-us/dotnet/api/system.uri.escapedatastring
        public static string EscapeString(this string toEscape)
        {
            int maxLengthAllowed = 32765;
            StringBuilder sb = new StringBuilder();
            int loops = toEscape.Length / maxLengthAllowed;

            for (int i = 0; i <= loops; i++)
                sb.Append(Uri.EscapeDataString(i < loops
                    ? toEscape.Substring(maxLengthAllowed * i, maxLengthAllowed)
                    : toEscape.Substring(maxLengthAllowed * i)));

            return sb.ToString();
        }

        // For unescaping strings there's no record of character limit. Just to be safe, I'm following the same limit
        // https://docs.microsoft.com/en-us/dotnet/api/system.uri.unescapedatastring
        public static string UnescapeString(this string dataString)
        {
            int limit = 32765;
            int charsProcessed = 0;

            var sb = new StringBuilder();

            while (dataString.Length > charsProcessed)
            {
                // Picking the part that's going through the unescaping process
                var toUnescape = dataString.Substring(charsProcessed).Length > limit
                    ? dataString.Substring(charsProcessed, limit)
                    : dataString.Substring(charsProcessed);

                // Checking if the last character of the string is in the middle of a toUnescape character. If that's the case, move back 4 characters
                var incorrectStrPos = toUnescape.Length == limit
                    ? toUnescape.IndexOf("%", toUnescape.Length - 4, StringComparison.InvariantCulture)
                    : -1;

                // If the position was wrong, substring to new position
                if (incorrectStrPos > -1)
                    toUnescape = dataString.Substring(charsProcessed).Length > incorrectStrPos
                        ? dataString.Substring(charsProcessed, incorrectStrPos)
                        : dataString.Substring(charsProcessed);

                sb.Append(Uri.UnescapeDataString(toUnescape));
                charsProcessed += toUnescape.Length;
            }

            return sb.ToString();
        }
        // A good discussion about this topic can be found at:
        // https://stackoverflow.com/questions/6695208/uri-escapedatastring-invalid-uri-the-uri-string-is-too-long

        // This is meant to prevent situation where special characters are inserted on fields meant to be file names
        public static string RemoveSpecialCharacters(this string str)
        {
            return str
                .Replace("&", " ")
                .Replace(@"""", "-")
                .Replace("?", "")
                .Replace("<", "-")
                .Replace(">", "-")
                .Replace("#", "")
                .Replace("{", "(")
                .Replace("}", ")")
                .Replace("%", " ")
                .Replace("~", "-")
                .Replace("/", "-")
                .Replace(@"\", "-");
        }
    }
}
