using System;

namespace Uroboro.Common.Models
{
    public abstract class BaseItem : IBaseItem
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override string ToString()
        {
            return $"Id=[{Id}],IsCompleted=[{IsCompleted}]";
        }
    }
}