using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlKata.Execution;
using Sync.Common;
using Sync.Model;

namespace Sync.Business
{
    public class SyncService : ISyncService
    {
        private readonly WebDocument _webDocument;
        private readonly ICustomDateFormat _customDateFormat;
        private readonly ProjectSettings _projectSettings;
        private readonly QueryFactory _queryFactory;

        public SyncService(WebDocument webDocument, ICustomDateFormat customDateFormat,
            ProjectSettings projectSettings,QueryFactory queryFactory)
        {
            _webDocument = webDocument;
            _customDateFormat = customDateFormat;
            _projectSettings = projectSettings;
            _queryFactory = queryFactory;
        }

        /// <summary>
        /// Get All Days Of Selected Years
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public IEnumerable<DateTime> GetDaysOfYears(int[] years)
        {
            return _customDateFormat.GetDaysOfYears(years);
        }


        public async Task SyncData(DateTime date)
        {
            var customFormattedDate = _customDateFormat.ToWikiFormat(date);

            var baseUrl = string.Format(_projectSettings.WikiSourceUrl, customFormattedDate);

            var items = _webDocument.GetSelectNodes(baseUrl, ProjectConst.ActionConst.Event);
            {
                foreach (var eventItem in items)
                {
                    var newHistory = new History()
                    {
                        Title = eventItem.InnerText,
                        FullContent = eventItem.InnerHtml,
                        HistoryDate = date,
                        Summary = string.Empty,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        LanguageType = LanguageType.Tr,
                        ActionType = ActionType.Event
                    };

                    var result = await CreateHistory(newHistory);
                }
            }
        }

        private async Task<Guid> CreateHistory(History newHistory)
        {
            return await _queryFactory.Query("history").InsertGetIdAsync<Guid>(newHistory);
        }
    }
}