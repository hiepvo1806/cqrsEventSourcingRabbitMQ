using InstratructureLayer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandStack.FoodStoreEvent
{
    public class StoreCreatedEvent : Event
    {
        public StoreCreatedEvent(string name,string link, Guid aggregateId)
        {
            Name = name;
            Link = link;
            AggregateId = aggregateId;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        
    }
}
