using System;
using MediatR;
namespace InstratructureLayer.Events
{
    public abstract class BaseMessage : IRequest
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected BaseMessage()
        {
            MessageType = GetType().Name;
        }
    }
}
