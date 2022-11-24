using System;

namespace StartupProject.Services.Helpers.CustomDate
{
    internal class EnglishDateService : DateHelper
    {
        public static readonly string[] EngShortMonths = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public static readonly string[] EngFullMonths = { "", "January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        protected override bool IsEnglishDate => true;

        public override string GetMonthName(int month)
        {
            return EngFullMonths[month];
        }

        public override string GetShortMonthName(int month)
        {
            return EngShortMonths[month];
        }

        public override (int Year, int Month, int Day) GetLastDateOfThisMonth(int month, int year)
        {
            return (year, month, DateTime.DaysInMonth(year, month));
        }

        public override int GetTotalDaysInTheMonth(int month, int year)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public override ((int Year, int Month, int Day) StartDateOfTheMonth, (int Year, int Month, int Day) LastDateOfTheMonth) GetCurrentMonth()
        {
            var (Year, Month, _) = GetCurrentDateTime();
            (int Year, int Month, int Day) lastDate = GetLastDateOfThisMonth(Month, Year);
            var firstDate = GetFirstDateOfThisMonth(Month, Year);

            return ((firstDate.Year, firstDate.Month, firstDate.Day), (lastDate.Year, lastDate.Month, lastDate.Day));
        }

        public override (int Year, int Month, int Day) GetCurrentDateTime()
        {
            return (CurrentEngDateTime.Year, CurrentEngDateTime.Month, CurrentEngDateTime.Day);
        }
    }
}
