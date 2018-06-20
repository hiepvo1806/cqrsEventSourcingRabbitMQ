using CommandStack.FoodStoreEvent;
using InstratructureLayer.MessageService;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandStack.FoodStoreEventHandler
{
    public class FoodStoreEventHandler : 
        INotificationHandler<StoreCreatedEvent>,
        INotificationHandler<StoreUpdatedEvent>,
        INotificationHandler<StoreDeletedEvent>
    {
        private IMessageNotify<string> _messageNotify;

        public FoodStoreEventHandler(IMessageNotify<string> messageNotify)
        {
            _messageNotify = messageNotify;
        }
        public Task Handle(StoreCreatedEvent notification, CancellationToken cancellationToken)
        {
            _messageNotify.NotifyService(JsonConvert.SerializeObject(notification));
            return Task.CompletedTask;
        }

        public Task Handle(StoreUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _messageNotify.NotifyService(JsonConvert.SerializeObject(notification));
            return Task.CompletedTask;
        }

        public Task Handle(StoreDeletedEvent notification, CancellationToken cancellationToken)
        {
            _messageNotify.NotifyService(JsonConvert.SerializeObject(notification));
            return Task.CompletedTask;
        }
    }
}
