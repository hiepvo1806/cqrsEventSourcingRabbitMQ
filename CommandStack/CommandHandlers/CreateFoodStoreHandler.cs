using AutoMapper;
using CommandStack.Commands;
using CommandStack.FoodStoreEvent;
using InstratructureLayer.Bus;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;
using InstratructureLayer.EventSourcing.Bus;
using InstratructureLayer.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandStack.CommandHandlers
{
    public class CreateFoodStoreHandler : CommandHandler, IRequestHandler<CreateFoodStoreCommand, Unit>

    {
        private IFoodStoreRepository _repo;

        public CreateFoodStoreHandler(
            IFoodStoreRepository repo,
            IUnitOfWork uow, IMediatorHandler bus, IMapper mapper) : base(uow, bus,mapper)
        {
            _repo = repo;
        }

        public Task<Unit> Handle(CreateFoodStoreCommand cmd, CancellationToken cancellationToken)
        {
            if (!cmd.IsValid())
            {
                throw new Exception();
            }
            var newItem = FoodStoreDomain.Create(cmd.Name, cmd.Link);
            var entity = _mapper.Map<FoodStore>(newItem);
            var entityId = _repo.Add(entity);

            if (_uow.Commit())
            {
                _bus.RaiseEvent(new StoreCreatedEvent(newItem.Name, newItem.Link, entityId));
                
            }
            return Task.FromResult(new Unit());
        }
    }
}
