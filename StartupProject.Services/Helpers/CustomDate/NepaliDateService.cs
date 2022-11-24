using StartupProject.Core.Infrastructure.Helper;
using System;

namespace StartupProject.Services.Helpers.CustomDate
{
    internal class NepaliDateService : DateHelper
    {
        public static readonly string[] NepMonths = { "", "Baisakh", "Jestha", "Ashadh", "Shrawan", "Bhadra", "Ashwin", "Kartik", "Mangsir", "Poush", "Magh", "Falgun", "Chaitra" };
        public static readonly string[] NepShortMonths = { "", "Bai", "Jes", "Ash", "Shr", "Bha", "Ash", "Kar", "Man", "Pou", "Mag", "Fal", "Chai" };

        protected override bool IsEnglishDate => false;

        public override string GetMonthName(int month)
        {
            return NepMonths[month];
        }

        public override string GetShortMonthName(int month)
        {
            return NepShortMonths[month];
        }

        public override (int Year, int Month, int Day) GetLastDateOfThisMonth(int month, int year)
        {
            int totalDaysInMonth = GetTotalDaysInTheMonth(month, year);
            return (year, month, totalDaysInMonth);
        }

        public override ((int Year, int Month, int Day) StartDateOfTheMonth, (int Year, int Month, int Day) LastDateOfTheMonth) GetCurrentMonth()
        {
            var toDay = GetCurrentDateTime();
            var firstDate = GetFirstDateOfThisMonth(toDay.Month, toDay.Year);
            var (year, month, day) = GetLastDateOfThisMonth(toDay.Month, toDay.Year);
            return ((firstDate.Year, firstDate.Month, firstDate.Day), (year, month, day));
        }

        private DateTime EngCurrentDateTime()
        {
            return DateTimeFormatter.CurrentDate;
        }

        public override (int Year, int Month, int Day) GetCurrentDateTime()
        {
            var date = EngCurrentDateTime();
            //Check for nepali date range that only be converted by the code
            // *Changed from 1944 to 1914 and, 2033 to 2053
            if (date.Year < 1913 || date.Year > 2053)
                throw new ArgumentException("Year must be between 1914-2053.");
            if (date.Month < 1 || date.Month > 12)
                throw new ArgumentException("Month must be between 1-12.");
            if (date.Day < 1 || date.Day > 31)
                throw new ArgumentException("Day must be between 1-31.");

            return GetDate(date);
        }

        public override int GetTotalDaysInTheMonth(int month, int year)
        {
            int totalDays;

            var index = year - 1970;
            if (Months.GetLength(0) > index)
                totalDays = Months[index, month];
            else
                // invalid year, should throw error and stop processing as if proced further invalid calculation may occur
                return 0;

            return totalDays;
        }

        private static (int Year, int Month, int Day) GetDate(DateTime date)
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

            int j;

            int i;
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
            return (y, m, total_nDays);
        }

    }
}
