using MediatR;
using System;

namespace InstratructureLayer.Events
{
    public abstract class Event : BaseMessage, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
