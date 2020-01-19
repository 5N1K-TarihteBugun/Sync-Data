namespace SyncData.Data
{
    public class History : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string FullContent { get; set; }
        public byte ActionTypeId { get; set; }
        public ActionType ActionType { get; set; }

        public byte LanguageTypeId { get; set; }
        public LanguageType LanguageType { get; set; }
    }
}