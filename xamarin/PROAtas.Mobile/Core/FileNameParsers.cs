using System.Text.RegularExpressions;

namespace PROAtas.Mobile.Core
{
    public static class FileNameParsers
    {
        static readonly string invalidFileNamePattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";

        public static bool IsFileNameValid(this string text)
        {
            return Regex.IsMatch(text, invalidFileNamePattern, RegexOptions.CultureInvariant);
        }

        public static string GetValidFileName(this string text)
        {
            var regex = new Regex(string.Format("[{0}]", Regex.Escape(invalidFileNamePattern)));
            return regex.Replace(text, "");
        }
    }
}
