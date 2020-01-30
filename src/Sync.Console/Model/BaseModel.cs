using System;
using SqlKata;

namespace Sync.Console.Model
{
    public class BaseModel
    {
        [Key("id")]
        public Guid Id { get; set; }
        
        [Column("create_date")] public DateTime CreateDate { get; set; }

        [Column("update_date")] public DateTime UpdateDate { get; set; }

        [Column("is_active")] public bool IsActive { get; set; }

        [Column("is_deleted")] public bool IsDeleted { get; set; }
    }
}