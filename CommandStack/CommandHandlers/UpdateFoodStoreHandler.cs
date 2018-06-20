using AutoMapper;
using CommandStack.Commands;
using CommandStack.EventSourceManagement;
using CommandStack.FoodStoreEvent;
using InstratructureLayer.Bus;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;
using InstratructureLayer.EventSourcing.Bus;
using InstratructureLayer.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandStack.CommandHandlers
{
    public class UpdateFoodStoreHandler : CommandHandler, IRequestHandler<UpdateFoodStoreCommand, Unit>
    {
        private IFoodStoreRepository _repo;
        private IFoodStoreEventSourceManager _foodStoreEventSourceManager;

        public UpdateFoodStoreHandler(
            IFoodStoreEventSourceManager foodStoreEventSourceManager,
            IFoodStoreRepository repo,
            IUnitOfWork uow, 
            IMediatorHandler bus, 
            IMapper mapper) : base(uow, bus, mapper)
        {
            _repo = repo;
            _foodStoreEventSourceManager = foodStoreEventSourceManager;
        }

        public Task<Unit> Handle(UpdateFoodStoreCommand cmd, CancellationToken cancellationToken)
        {
            if (!cmd.IsValid())
            {
                throw new Exception();
            }
            var source = _foodStoreEventSourceManager.ReplayEntity(cmd.Id);
            var newItem = FoodStoreDomain.Update(source, cmd.Name,cmd.Link);
            var entity = _mapper.Map<FoodStore>(newItem);
            entity.Id = cmd.Id;
            _repo.Update(entity);

            if (_uow.Commit())
            {
                _bus.RaiseEvent(new StoreUpdatedEvent(newItem.Name, newItem.Link, cmd.Id));

            }
            return Task.FromResult(new Unit());
        }
    }
}
