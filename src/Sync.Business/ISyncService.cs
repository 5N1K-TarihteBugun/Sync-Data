using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Model;

namespace Sync.Business
{
    public interface ISyncService
    {
        IEnumerable<DateTime> GetDaysOfYears(int[] years);
        Task SyncData(DateTime date);
    }
}