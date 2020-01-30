using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sync.Common
{
    public class CustomDateFormat : ICustomDateFormat
    {
        private readonly string _culture;

        public CustomDateFormat(string culture)
        {
            _culture = culture;
        }

        public string GetDay(int year, int month, int day)
        {
            return ToWikiFormat(new DateTime(year, month, day));
        }

        public string ToWikiFormat(DateTime date)
        {
            return date.ToString("d_MMMM", new CultureInfo(_culture));
            ;
        }

        public IList<DateTime> GetDaysOfYears(int[] years)
        {
            var allDaysOfYears = new List<DateTime>();

            foreach (var year in years) allDaysOfYears.AddRange(GetDaysOfYear(year));

            return allDaysOfYears;
        }

        public IList<DateTime> GetDaysOfYear(int year)
        {
            var days = new List<DateTime>();

            for (var month = 1; month <= 12; month++)
            {
                var day = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                    .Select(day => new DateTime(year, month, day));
                days.AddRange(day);
            }

            return days;
        }
    }
}