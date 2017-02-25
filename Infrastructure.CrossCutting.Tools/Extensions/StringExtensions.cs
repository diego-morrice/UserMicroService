using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.CrossCutting.Tools.Extensions
{
    public static class StringExtensions
    {
        public static string ToFormat(this string str, params object[] values)
        {
            return string.Format(str, values);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNotNullNorWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLowerInvariant());
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            var r = new Regex("(?:[^a-z0-9A-ZÁÉÍÓÚÂÊÔÀÔÃÇáéíóúâêôàõãç ]|(?<=['\"])s)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, string.Empty);
        }

        public static string RemoveAccents(this string str)
        {
            const string vWithAccent = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÒÓÔÕÖÙÚÛÜàáâãäåçèéêëìíîïòóôõöùúûü";
            const string vWithoutAccent = "AAAAAACEEEEIIIIOOOOOUUUUaaaaaaceeeeiiiiooooouuuu";
            var finalString = "";

            for (var i = 1; (i <= str.Length); i++)
            {
                var vPos = (vWithAccent.IndexOf(str.Substring((i - 1), 1), 0, StringComparison.Ordinal) + 1);

                if ((vPos > 0))
                {
                    finalString += vWithoutAccent.Substring((vPos - 1), 1);
                }
                else
                {
                    finalString += str.Substring((i - 1), 1);
                }
            }
            return finalString;
        }

        public static string StripWordsWithLessThanXLetters(this string input, int x)
        {
            var inputElements = input.Split(' ');
            var resultBuilder = new StringBuilder();
            foreach (var element in inputElements)
            {
                if (element.Length >= x)
                {
                    resultBuilder.Append(element + " ");
                }
            }
            return resultBuilder.ToString().Trim();
        }

        public static string Truncate(this string str, int limite)
        {
            return str.IsNullOrWhiteSpace() ? str : str.Substring(0, Math.Min(limite, str.Length));
        }
    }
}