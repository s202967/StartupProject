using System;
using System.ComponentModel.DataAnnotations;

namespace StartupProject.Client.Filters
{
    /// <summary>
    /// Data Annotations checking for date to equal or be more than curernt date
    /// </summary>
    public class DateMustLessThanCurrentDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// Is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var dt = (DateTime)value;
            if (dt != null && dt >= DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
