using System.Text.RegularExpressions;

namespace PROAtas.Core
{
    public class TextParsers
    {
        static string invalidFileNamePattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";

        public static bool IsFileNameValid(string text)
        {
            return Regex.IsMatch(text, invalidFileNamePattern, RegexOptions.CultureInvariant);
        }

        public static string GetValidFileName(string text)
        {
            var regex = new Regex(string.Format("[{0}]", Regex.Escape(invalidFileNamePattern)));
            return regex.Replace(text, "");
        }
    }
}
