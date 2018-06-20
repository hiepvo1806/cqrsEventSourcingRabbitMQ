using AutoMapper;
using InstratructureLayer.Bus;
using InstratructureLayer.Entity;
using InstratructureLayer.Events;
using InstratructureLayer.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.EventSourcing.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventStore;
        public InMemoryBus(
            IEventRepository eventStore, 
            IMediator mediator,
            IMapper mapper
            
            )
        {
            _eventStore = eventStore;
            _mediator = mediator;
            _mapper = mapper;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
            {
                var eventEnt = _mapper.Map<EventEntity>(@event);
                _eventStore?.Store(eventEnt);
            }
            return _mediator.Publish(@event);
        }
    }
}
