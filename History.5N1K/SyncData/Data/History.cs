using System;
using Newtonsoft.Json;

namespace SyncData.Data
{
    public class History : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        //public string Summary { get; set; }
        public string FullContent { get; set; }
        public byte ActionTypeId { get; set; }

        [JsonIgnore]
        public ActionType ActionType
        {
            get => (ActionType) ActionTypeId;
            set => ActionTypeId = (byte) value;
        }

        public byte LanguageTypeId { get; set; }

        [JsonIgnore]
        public LanguageType LanguageType
        {
            get => (LanguageType) LanguageTypeId;
            set => LanguageTypeId = (byte) value;
        }

        [JsonIgnore]
        public DateTime HistoryDate
        {
            get => new DateTime(HistoryYear,HistoryMonth,HistoryDay);
            set
            {
                HistoryDay = value.Day;
                HistoryMonth = value.Month;
                HistoryYear = value.Year;
            }
        }

        public int HistoryDay { get; set; }
        public int HistoryMonth { get; set; }
        public int HistoryYear { get; set; }
        
    }
}