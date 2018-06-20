using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.Entity
{
    public class EventEntity : BaseEntity<Guid>
    {
        public EventType Type { get; set; }
        public string JsonData { get; set; }
        public string ObjType { get; set; }

        public string AggId { get; set; }
    }

    public enum EventType
    {
        Create,
        Update,
        Delete
    }
}
