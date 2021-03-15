using System;

namespace Uroboro.Common.Models
{
    public interface IBaseItem
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string CompletedBy { get; set; }
        //public DateTime? CompletedAt { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime? ModifiedAt { get; set; }
        //public string DeletedBy { get; set; }
        //public DateTime? DeletedAt { get; set; }
    }
}