using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client;
using SyncData.Common;
using SyncData.Data;

namespace SyncData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string graphQlUrl = "https://history-5n1k.herokuapp.com/v1/graphql";
            const string urlFormat = @"https://tr.wikipedia.org/wiki/{0}";
            var dateFormat = new DateFormat("tr-TR");
            var dates = dateFormat.GetDaysOfYear(2012);

            var client = new GraphQlDataClient(graphQlUrl);

            var historyItems = new List<History>();

            foreach (var date in dates)
            {
                var document = new WebDocument(string.Format(urlFormat, dateFormat.ToWikiFormat(date)));

                var eventItems = document.GetSelectNodes(ActionRegex.Events);

                foreach (var eventItem in eventItems)
                {
                    var newHistory = new History()
                    {
                        Id = Guid.NewGuid(),
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

                    var result = await client.CreateHistory(newHistory);
                    Console.WriteLine(result);
                    //historyItems.Add(newHistory);
                }
            }
        }
    }
}