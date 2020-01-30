using System;
using SqlKata;
using Column= SqlKata.Column;

namespace Sync.Console.Model
{
    public class History : BaseModel
    {
        [Column("title")] 
        public string Title { get; set; }

        [Column("summary")] 
        public string Summary { get; set; }

        [Column("full_content")] 
        public string FullContent { get; set; }

        [Column("action_type_id")] 
        public byte ActionTypeId { get; set; }

        [Column("language_type_id")] 
        public byte LanguageTypeId { get; set; }

        [Column("history_day")] 
        public int HistoryDay { get; set; }

        [Column("history_month")] 
        public int HistoryMonth { get; set; }

        [Column("history_year")] 
        public int HistoryYear { get; set; }

        [Ignore]
        public LanguageType LanguageType
        {
            get => (LanguageType) LanguageTypeId;
            set => LanguageTypeId = (byte) value;
        }

        [Ignore]
        public DateTime HistoryDate
        {
            get => new DateTime(HistoryYear, HistoryMonth, HistoryDay);
            set
            {
                HistoryDay = value.Day;
                HistoryMonth = value.Month;
                HistoryYear = value.Year;
            }
        }

        [Ignore]
        public ActionType ActionType
        {
            get => (ActionType) ActionTypeId;
            set => ActionTypeId = (byte) value;
        }
    }
}