using System.Text.RegularExpressions;

namespace Application.Helpers
{
    public static class HtmlHelpers
    {
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
