using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sync.Console.Business
{
    public interface ISyncService
    {
        IEnumerable<DateTime> GetDaysOfYears(int[] years);
        Task SyncData(DateTime date);
    }
}