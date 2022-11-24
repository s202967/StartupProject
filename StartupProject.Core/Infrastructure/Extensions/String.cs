using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupProject.Core.Infrastructure.Extensions
{
    /// <summary>
    /// string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// join string array with separator
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string SmartJoin(this string separator, params string[] items)
        {
            return string.Join(separator, items.Where(x => !string.IsNullOrEmpty(x)).ToArray());
        }

        /// <summary>
        /// check string contains a substring
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// convert single string to list
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> ToStringList(this string source)
        {
            return new List<string> { source };
        }

        /// <summary>
        /// format string
        /// </summary>
        /// <param name="message"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string FormatString(this string message, params string[] items)
        {
            if (string.IsNullOrEmpty(message))
                return string.Empty;
            return string.Format(message, items);
        }

        /// <summary>
        /// Indicates whether this string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}