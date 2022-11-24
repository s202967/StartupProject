using StartupProject.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace StartupProject.Client.Extension.System_
{
    /// <summary>
    /// Extension for model validation
    /// </summary>
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// gets error list
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static List<string> GetModelErrors(this ModelStateDictionary dict)
        {
            var modelErrors = dict.Keys.SelectMany(k => dict[k].Errors)
                .Select(m => m.ErrorMessage)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .ToList();
            return modelErrors;
        }

        /// <summary>
        /// Join error list to string
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string GetModelErrorText(this ModelStateDictionary dict, string delimiter)
        {
            return delimiter.SmartJoin(dict.GetModelErrors().ToArray());
        }
    }
}