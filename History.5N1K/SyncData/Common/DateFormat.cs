using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SyncData.Common
{
    public class DateFormat
    {
        private readonly string _culture;

        public DateFormat(string culture)
        {
            _culture = culture;
        }

        public string GetDay(int year, int month, int day)
        {
            return new DateTime(year, month, day).ToWikiFormat(_culture);
        }

        public string ToWikiFormat(DateTime date)
        {
            return date.ToWikiFormat(_culture);
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