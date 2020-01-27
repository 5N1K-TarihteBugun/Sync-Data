using System;
using System.Collections.Generic;

namespace Sync.Common
{
    public interface ICustomDateFormat
    {
        string GetDay(int year, int month, int day);
        string ToWikiFormat(DateTime date);
        IList<DateTime> GetDaysOfYears(int[] years);
        IList<DateTime> GetDaysOfYear(int year);
    }
}