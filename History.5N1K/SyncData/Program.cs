using System;
using System.Collections.Generic;
using SyncData.Common;
using SyncData.Data;

namespace SyncData
{
    class Program
    {
        static void Main(string[] args)
        {
            const string hasuraUrl = "https://history-5n1k.herokuapp.com/v1/graphql";
            const string urlFormat = @"https://tr.wikipedia.org/wiki/{0}";
            var dateFormat = new DateFormat("tr-TR");
            var dates = dateFormat.GetDaysOfYear(2012);

            var historyItems = new List<History>();

            foreach (var date in dates)
            {
                var document = new WebDocument(string.Format(urlFormat,dateFormat.ToWikiFormat(date)));

                var eventItems = document.GetSelectNodes(ActionRegex.Events);

                foreach (var eventItem in eventItems)
                {
                    var newHistory = new History()
                    {
                        Title = eventItem.InnerText,
                        FullContent = eventItem.InnerHtml,
                        HistoryDate = date,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                        LanguageType = LanguageType.Tr,
                        ActionType = ActionType.Event
                    };
                    
                    historyItems.Add(newHistory);
                }

            }
        }
    }
}