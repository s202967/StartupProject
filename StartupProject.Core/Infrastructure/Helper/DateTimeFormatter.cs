using System;

namespace StartupProject.Core.Infrastructure.Helper
{
    public static class DateTimeFormatter
    {
        public static DateTime CurrentDate => DateTime.Now;

        public static DateTime CurrentDateTimeUtc => DateTime.UtcNow;

        public static string ToMMMddyyyy(this DateTime? datetime)
        {
            return datetime?.ToString("MMM dd, yyyy");
        }

        public static string ToMMMddyyyy(this DateTime datetime)
        {
            return datetime.ToString("MMM dd, yyyy");
        }

        public static string ToddMMMMyyyy(this DateTime datetime)
        {
            return datetime.ToString("dd MMMM, yyyy");
        }

        public static string ToddMMMMyyyy(this DateTime? datetime)
        {
            return datetime?.ToString("dd MMMM, yyyy");
        }

        public static DateTime? NepToEngDate(this string dateValue)
        {
            if (string.IsNullOrEmpty(dateValue))
                return null;

            var values = dateValue.Split('/');
            var cDate = new DateTime(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
            return cDate;
        }

        public static string ToCompatibleDate(this DateTime dateValue)
        {
            return $"{dateValue.Year}/{dateValue.Month}/{dateValue.Month}";
        }
        public static string ToMMMdd(this DateTime? datetime)
        {
            return datetime?.ToString("MMM dd");
        }

        public static string ToMMMdd(this DateTime datetime)
        {
            return datetime.ToString("MMM dd");
        }

        public static string Todd(this DateTime? datetime)
        {
            return datetime?.ToString("dd");
        }

        public static string Todd(this DateTime datetime)
        {
            return datetime.ToString("dd");
        }
    }
}
