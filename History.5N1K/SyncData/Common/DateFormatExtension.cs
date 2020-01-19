using System;
using System.Globalization;

namespace SyncData.Common
{
    public static class DateExtension
    {
        public static string ToWikiFormat(this DateTime dateTime, string culture)
        {
           return dateTime.ToString("d_MMMM", new CultureInfo(culture));
        }
    }
}