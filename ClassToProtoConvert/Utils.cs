using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassToProtoConvert
{
    public static class Utils
    {
        /// <summary>
        /// String replace function that support
        /// </summary>
        /// <param name="OrigString">Original input string</param>
        /// <param name="FindString">The string that is to be replaced</param>
        /// <param name="ReplaceWith">The replacement string</param>
        /// <param name="Instance">Instance of the FindString that is to be found. if Instance = -1 all are replaced</param>
        /// <param name="CaseInsensitive">Case insensitivity flag</param>
        /// <returns>updated string or original string if no matches</returns>
        public static string ReplaceStringInstance(string OrigString, string FindString,
                                                   string ReplaceWith, int Instance,
                                                   bool CaseInsensitive)
        {
            if (Instance == -1)
                return ReplaceString(OrigString, FindString, ReplaceWith, CaseInsensitive);

            int at1 = 0;
            for (int x = 0; x < Instance; x++)
            {

                if (CaseInsensitive)
                    at1 = OrigString.IndexOf(FindString, at1, OrigString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = OrigString.IndexOf(FindString, at1);

                if (at1 == -1)
                    return OrigString;

                if (x < Instance - 1)
                    at1 += FindString.Length;
            }

            return OrigString.Substring(0, at1) + ReplaceWith + OrigString.Substring(at1 + FindString.Length);

            //StringBuilder sb = new StringBuilder(OrigString);
            //sb.Replace(FindString, ReplaceString, at1, FindString.Length);
            //return sb.ToString();
        }

        /// <summary>
        /// Replaces a substring within a string with another substring with optional case sensitivity turned off.
        /// </summary>
        /// <param name="OrigString">String to do replacements on</param>
        /// <param name="FindString">The string to find</param>
        /// <param name="ReplaceString">The string to replace found string wiht</param>
        /// <param name="CaseInsensitive">If true case insensitive search is performed</param>
        /// <returns>updated string or original string if no matches</returns>
        public static string ReplaceString(string OrigString, string FindString,
                                           string ReplaceString, bool CaseInsensitive)
        {
            int at1 = 0;
            while (true)
            {
                if (CaseInsensitive)
                    at1 = OrigString.IndexOf(FindString, at1, OrigString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = OrigString.IndexOf(FindString, at1);

                if (at1 == -1)
                    return OrigString;

                if (at1 - 1 >= 0 && (at1 + FindString.Length) < OrigString.Length)
                {
                    if (Char.IsWhiteSpace(OrigString[at1 - 1]) && Char.IsWhiteSpace(OrigString[at1 + FindString.Length]))
                    {
                        OrigString = OrigString.Substring(0, at1) + ReplaceString + OrigString.Substring(at1 + FindString.Length);
                    }

                    at1 += ReplaceString.Length;

                }
                else
                {
                    at1 += 1;
                }

            }

            return OrigString;
        }
    }
}
