using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;

namespace Sync.Model
{
    public class History : BaseModel
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Summary { get; set; }
        
        public string FullContent { get; set; }
        
        public byte ActionTypeId { get; set; }
        
        [Write(false)]
        [Computed]
        public ActionType ActionType
        {
            get => (ActionType) ActionTypeId;
            set => ActionTypeId = (byte) value;
        }

        public byte LanguageTypeId { get; set; }
        
        [Write(false)]
        [Computed]
        public LanguageType LanguageType
        {
            get => (LanguageType) LanguageTypeId;
            set => LanguageTypeId = (byte) value;
        }
        
        [Write(false)]
        [Computed]
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