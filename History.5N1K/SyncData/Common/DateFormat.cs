using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SyncData
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
            return new DateTime(year, month, day).ToString("d_MMMM", new CultureInfo(_culture));
        }
        
        public IList<string> GetDaysOfYear(int year)
        {
            var days = new List<string>();

            for (var month = 1; month <= 12; month++)
            {
                var day = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                    .Select(day => GetDay(year, month, day));
                days.AddRange(day);
            }

            return days;
        }
    }
}