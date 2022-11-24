using StartupProject.Core.Infrastructure.Helper;
using System;

namespace StartupProject.Services.Helpers.CustomDate
{
    public abstract class DateHelper
    {
        protected abstract bool IsEnglishDate { get; }

        public abstract string GetMonthName(int month);

        public abstract string GetShortMonthName(int month);

        public abstract (int Year, int Month, int Day) GetCurrentDateTime();

        public abstract int GetTotalDaysInTheMonth(int month, int year);

        public abstract (int Year, int Month, int Day) GetLastDateOfThisMonth(int month, int year);

        public static DateTime GetFirstDateOfThisMonth(int month, int year)
        {
            return new DateTime(year, month, 1);
        }

        public abstract ((int Year, int Month, int Day) StartDateOfTheMonth, (int Year, int Month, int Day) LastDateOfTheMonth) GetCurrentMonth();

        public (int Year, int Month, int Day) GetDateTimeFromString(string dateValue)
        {
            dateValue = dateValue.Replace('-', '/');
            var values = dateValue.Split('/');
            return (int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
        }

        public static DateTime GetStartDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        public static DateTime GetEndDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public DateTime ConvertToEngDate(int year, int month, int day)
        {
            if (IsEnglishDate) return new DateTime(year, month, day);

            if (month < 1 || month > 12)
                throw new ArgumentException("Month must be between 1-12.");
            if (day < 1 || day > 32)
                throw new ArgumentException("Day must be between 1-32.");
            return GetEngDate(year, month, day);
        }

        public string ConvertToNepDate(DateTime dateTime)
        {
            if (IsEnglishDate) return CompatibleDate(dateTime.Year, dateTime.Month, dateTime.Day);

            if (dateTime.Month < 1 || dateTime.Month > 12)
                throw new ArgumentException("Month must be between 1-12.");
            if (dateTime.Day < 1 || dateTime.Day > 32)
                throw new ArgumentException("Day must be between 1-32.");
            return GetNepDate(dateTime);
        }

        private string GetNepDate(DateTime date)
        {
            int dd = date.Day;
            int mm = date.Month;
            int yy = date.Year;

            // english month data.
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int def_eyy = 1914; //spear head english date... *Changed from 1944 to 1914
            int def_nyy = 1970; // *Changed from 2000
            int def_nmm = 9;
            int def_ndd = 18 - 1; //spear head nepali date... *Original 17-1
            int total_eDays = 0;
            int day = 5 - 1; //all the initializations...

            int i;
            int j;
            // count total no. of days in-terms of year
            for (i = 0; i < yy - def_eyy; i++)
            {
                if (DateTime.IsLeapYear(def_eyy + i))
                {
                    for (j = 0; j < 12; j++)
                        total_eDays += lmonth[j];
                }
                else
                {
                    for (j = 0; j < 12; j++)
                        total_eDays += month[j];
                }
            }

            // count total no. of days in-terms of month			
            for (i = 0; i < mm - 1; i++)
            {
                if (DateTime.IsLeapYear(yy))
                    total_eDays += lmonth[i];
                else
                    total_eDays += month[i];
            }

            // count total no. of days in-terms of date
            total_eDays += dd;

            i = 0;
            j = def_nmm;
            int total_nDays = def_ndd;
            int m = def_nmm;
            int y = def_nyy;

            while (total_eDays != 0)
            {
                int a = Months[i, j];
                total_nDays++;
                day++;
                if (total_nDays > a)
                {
                    m++;
                    total_nDays = 1;
                    j++;
                }
                if (day > 7)
                    day = 1;
                if (m > 12)
                {
                    y++;
                    m = 1;
                }
                if (j > 12)
                {
                    j = 1;
                    i++;
                }
                total_eDays--;
            }

            //set new eng date
            return CompatibleDate(y, m, total_nDays);
        }

        public string GetAppropriateDate(DateTime datetime)
        {
            if (IsEnglishDate)
            {
                return datetime.Year + "/" + datetime.Month.ToString().PadLeft(2, '0') + "/" + datetime.Day.ToString().PadLeft(2, '0');
            }

            var date = new DateTime(datetime.Year, datetime.Month, datetime.Day);
            return ConvertToNepDate(date);
        }

        public DateTime CurrentEngDateTime
        {
            get
            {
                return DateTimeFormatter.CurrentDate;
            }
        }

        public string CompatibleDate(int year, int month, int day)
        {
            return $"{year}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// Days in each month of each year for nepali date
        /// </summary>
        protected static readonly int[,] Months =
  {
            {1970, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1971, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30},
            {1972, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30},
            {1973, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31},
            {1974, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1975, 31, 31, 32, 32, 31, 31, 30, 29, 30, 29, 30, 30},
            {1976, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31},
            {1977, 30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31},
            {1978, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1979, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30},
            {1980, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31},
            {1981, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30},
            {1982, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1983, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30},
            {1984, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31},
            {1985, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30},
            {1986, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1987, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30},
            {1988, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31},
            {1989, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30},
            {1990, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1991, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30},
            {1992, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31},
            {1993, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30},
            {1994, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1995, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30},
            {1996, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31},
            {1997, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1998, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {1999, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31},
            {2000,30,32,31,32,31,30,30,30,29,30,29,31},
            {2001,31,31,32,31,31,31,30,29,30,29,30,30},
            {2002,31,31,32,32,31,30,30,29,30,29,30,30},
            {2003,31,32,31,32,31,30,30,30,29,29,30,31},
            {2004,30,32,31,32,31,30,30,30,29,30,29,31},
            {2005,31,31,32,31,31,31,30,29,30,29,30,30},
            {2006,31,31,32,32,31,30,30,29,30,29,30,30},
            {2007,31,32,31,32,31,30,30,30,29,29,30,31},
            {2008,31,31,31,32,31,31,29,30,30,29,29,31},
            {2009,31,31,32,31,31,31,30,29,30,29,30,30},
            {2010,31,31,32,32,31,30,30,29,30,29,30,30},
            {2011,31,32,31,32,31,30,30,30,29,29,30,31},
            {2012,31,31,31,32,31,31,29,30,30,29,30,30},
            {2013,31,31,32,31,31,31,30,29,30,29,30,30},
            {2014,31,31,32,32,31,30,30,29,30,29,30,30},
            {2015,31,32,31,32,31,30,30,30,29,29,30,31},
            {2016,31,31,31,32,31,31,29,30,30,29,30,30},
            {2017,31,31,32,31,31,31,30,29,30,29,30,30},
            {2018,31,32,31,32,31,30,30,29,30,29,30,30},
            {2019,31,32,31,32,31,30,30,30,29,30,29,31},
            {2020,31,31,31,32,31,31,30,29,30,29,30,30},
            {2021,31,31,32,31,31,31,30,29,30,29,30,30},
            {2022,31,32,31,32,31,30,30,30,29,29,30,30},
            {2023,31,32,31,32,31,30,30,30,29,30,29,31},
            {2024,31,31,31,32,31,31,30,29,30,29,30,30},
            {2025,31,31,32,31,31,31,30,29,30,29,30,30},
            {2026,31,32,31,32,31,30,30,30,29,29,30,31},
            {2027,30,32,31,32,31,30,30,30,29,30,29,31},
            {2028,31,31,32,31,31,31,30,29,30,29,30,30},
            {2029,31,31,32,31,32,30,30,29,30,29,30,30},
            {2030,31,32,31,32,31,30,30,30,29,29,30,31},
            {2031,30,32,31,32,31,30,30,30,29,30,29,31},
            {2032,31,31,32,31,31,31,30,29,30,29,30,30},
            {2033,31,31,32,32,31,30,30,29,30,29,30,30},
            {2034,31,32,31,32,31,30,30,30,29,29,30,31},
            {2035,30,32,31,32,31,31,29,30,30,29,29,31},
            {2036,31,31,32,31,31,31,30,29,30,29,30,30},
            {2037,31,31,32,32,31,30,30,29,30,29,30,30},
            {2038,31,32,31,32,31,30,30,30,29,29,30,31},
            {2039,31,31,31,32,31,31,29,30,30,29,30,30},
            {2040,31,31,32,31,31,31,30,29,30,29,30,30},
            {2041,31,31,32,32,31,30,30,29,30,29,30,30},
            {2042,31,32,31,32,31,30,30,30,29,29,30,31},
            {2043,31,31,31,32,31,31,29,30,30,29,30,30},
            {2044,31,31,32,31,31,31,30,29,30,29,30,30},
            {2045,31,32,31,32,31,30,30,29,30,29,30,30},
            {2046,31,32,31,32,31,30,30,30,29,29,30,31},
            {2047,31,31,31,32,31,31,30,29,30,29,30,30},
            {2048,31,31,32,31,31,31,30,29,30,29,30,30},
            {2049,31,32,31,32,31,30,30,30,29,29,30,30},
            {2050,31,32,31,32,31,30,30,30,29,30,29,31},
            {2051,31,31,31,32,31,31,30,29,30,29,30,30},
            {2052,31,31,32,31,31,31,30,29,30,29,30,30},
            {2053,31,32,31,32,31,30,30,30,29,29,30,30},
            {2054,31,32,31,32,31,30,30,30,29,30,29,31},
            {2055,31,31,32,31,31,31,30,29,30,29,30,30},
            {2056,31,31,32,31,32,30,30,29,30,29,30,30},
            {2057,31,32,31,32,31,30,30,30,29,29,30,31},
            {2058,30,32,31,32,31,30,30,30,29,30,29,31},
            {2059,31,31,32,31,31,31,30,29,30,29,30,30},
            {2060,31,31,32,32,31,30,30,29,30,29,30,30},
            {2061,31,32,31,32,31,30,30,30,29,29,30,31},
            {2062,30,32,31,32,31,31,29,30,29,30,29,31},
            {2063,31,31,32,31,31,31,30,29,30,29,30,30},
            {2064,31,31,32,32,31,30,30,29,30,29,30,30},
            {2065,31,32,31,32,31,30,30,30,29,29,30,31},
            {2066,31,31,31,32,31,31,29,30,30,29,29,31},
            {2067,31,31,32,31,31,31,30,29,30,29,30,30},
            {2068,31,31,32,32,31,30,30,29,30,29,30,30},
            {2069,31,32,31,32,31,30,30,30,29,29,30,31},
            {2070,31,31,31,32,31,31,29,30,30,29,30,30},
            {2071,31,31,32,31,31,31,30,29,30,29,30,30},
            {2072,31,32,31,32,31,30,30,29,30,29,30,30},
            {2073,31,32,31,32,31,30,30,30,29,29,30,31},
            {2074,31,31,31,32,31,31,30,29,30,29,30,30},
            {2075,31,31,32,31,31,31,30,29,30,29,30,30},
            {2076,31,32,31,32,31,30,30,30,29,29,30,30},
            {2077,31,32,31,32,31,30,30,30,29,30,29,31},
            {2078,31,31,31,32,31,31,30,29,30,29,30,30},
            {2079,31,31,32,31,31,31,30,29,30,29,30,30},
            {2080,31,32,31,32,31,30,30,30,29,29,30,30},
            {2081, 31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2082, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2083, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30},
            {2084, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30},
            {2085, 31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30},
            {2086, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2087, 31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30},
            {2088, 30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30},
            {2089, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2090, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2091, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30},
            {2092, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {2093, 31, 32, 31, 31, 31, 31, 30, 29, 30, 29, 30, 31},
            {2094, 31, 31, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2095, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30},
            {2096, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {2097, 31, 32, 30, 31, 31, 31, 30, 30, 29, 30, 29, 31},
            {2098, 31, 31, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2099, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30},
            {2100, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30},
            {2101, 31, 32, 31, 31, 31, 31, 30, 30, 29, 30, 29, 31},
            {2102, 31, 31, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2103, 31, 31, 32, 31, 31, 30, 30, 30, 30, 29, 30, 30},
            {2104, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 31},
            {2105, 30, 32, 31, 31, 31, 31, 30, 30, 29, 30, 29, 31},
            {2106, 31, 31, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30},
            {2107, 31, 31, 32, 31, 31, 30, 30, 30, 30, 29, 30, 30},
            {2108, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 31},
            {2109, 30, 32, 31, 31, 31, 31, 30, 30, 29, 30, 29, 31},
            {2110, 31, 31, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30}
    };


        //public string ToString(DateTime date)
        //{
        //    //return Day + "/" + Month + "/" + Year;
        //    //if(Config.DateFormat_YYYY_MM_DD)
        //    return date.Year + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0');
        //    //return Day + "/" + Month + "/" + Year;
        //}
        private static DateTime GetEngDate(int yy, int mm, int dd)
        {
            // int yy = date.Year, mm = date.Month, dd = date.Day;

            int def_eyy = 1913; int def_emm = 4; int def_edd = 13 - 1;		// init english date. *Original 1943,4,14-1
            int def_nyy = 1970;         // equivalent nepali date.          // *Original 2000
            int total_nDays = 0;
            int day = 0 - 1;       // initializations...
            int k = 0;
            //may be the total months of the nep starting year
            int[] month = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] lmonth = { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int i;
            int j;
            // count total days in-terms of year
            for (i = 0; i < yy - def_nyy; i++)
            {
                for (j = 1; j <= 12; j++)
                {
                    total_nDays += Months[k, j];
                }
                k++;
            }

            // count total days in-terms of month	
            for (j = 1; j < mm; j++)
            {
                total_nDays += Months[k, j];
            }

            // count total days in-terms of dat
            total_nDays += dd;

            //calculation of equivalent english date...
            int total_eDays = def_edd;
            int m = def_emm;
            int y = def_eyy;
            while (total_nDays != 0)
            {
                int a;
                if (DateTime.IsLeapYear(y))
                {
                    a = lmonth[m];
                }
                else
                {
                    a = month[m];
                }
                total_eDays++;
                day++;
                if (total_eDays > a)
                {
                    m++;
                    total_eDays = 1;
                    if (m > 12)
                    {
                        y++;
                        m = 1;
                    }
                }
                if (day > 7)
                    day = 1;
                total_nDays--;
            }

            //set new eng date
            var engDate = new DateTime(y, m, total_eDays);
            return engDate;
        }

        public string GetElapsedTime(DateTime from, DateTime to)
        {
            GetElapsedTime(from, to, out int years, out int months, out _, out _,
                out _, out _, out _);
            string text = "";
            if (years != 0)
                text = " " + years + (years == 1 ? " year" : " years");
            if (months != 0)
                text += " " + months + (months == 1 ? " month" : " months");
            // if (days != 0)
            //    text += " " + days + " days";

            return text;
        }

        public Tuple<int, int> GetYearAndMonth(DateTime from, DateTime to)
        {
            GetYrMonth(from, to, out int years, out int months);
            return Tuple.Create(years, months);
        }

        private static void GetYrMonth(DateTime from, DateTime to, out int years, out int months)
        {
            GetElapsedTime(from, to, out years, out months,
                out _, out _, out _, out _, out _);
        }

        public static void GetElapsedTime(DateTime from_date, DateTime to_date,
            out int years, out int months, out int days, out int hours,
            out int minutes, out int seconds, out int milliseconds)
        {
            // If from_date > to_date, switch them around.
            if (from_date > to_date)
            {
                GetElapsedTime(to_date, from_date,
                    out years, out months, out days, out hours,
                    out minutes, out seconds, out milliseconds);
                years = -years;
                months = -months;
                days = -days;
                hours = -hours;
                minutes = -minutes;
                seconds = -seconds;
                milliseconds = -milliseconds;
            }
            else
            {
                // Handle the years.
                years = to_date.Year - from_date.Year;

                // See if we went too far.
                DateTime test_date = from_date.AddMonths(12 * years);
                if (test_date > to_date)
                {
                    years--;
                    test_date = from_date.AddMonths(12 * years);
                }

                // Add months until we go too far.
                months = 0;
                while (test_date <= to_date)
                {
                    months++;
                    test_date = from_date.AddMonths(12 * years + months);
                }
                months--;

                // Subtract to see how many more days,
                // hours, minutes, etc. we need.
                from_date = from_date.AddMonths(12 * years + months);
                TimeSpan remainder = to_date - from_date;
                days = remainder.Days;
                hours = remainder.Hours;
                minutes = remainder.Minutes;
                seconds = remainder.Seconds;
                milliseconds = remainder.Milliseconds;
            }
        }
    }
}
