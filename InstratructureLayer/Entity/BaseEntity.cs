using System;

namespace InstratructureLayer.Entity
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
        public byte[] Timestamp { get; set; }
    }

    public class Entity: BaseEntity<int>
    {

    }
}
