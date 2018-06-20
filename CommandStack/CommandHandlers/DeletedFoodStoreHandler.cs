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
using System.Threading;
using System.Threading.Tasks;

namespace CommandStack.CommandHandlers
{
    public class DeletedFoodStoreHandler : CommandHandler, IRequestHandler<DeleteFoodStoreCommand, Unit>
    {
        private IFoodStoreRepository _repo;
        private IFoodStoreEventSourceManager _foodStoreEventSourceManager;
        public DeletedFoodStoreHandler(IFoodStoreEventSourceManager foodStoreEventSourceManager,
            IFoodStoreRepository repo,
            IUnitOfWork uow,
            IMediatorHandler bus,
            IMapper mapper) : base(uow, bus, mapper)
        {
            _repo = repo;
            _foodStoreEventSourceManager = foodStoreEventSourceManager;
        }

        public Task<Unit> Handle(DeleteFoodStoreCommand cmd, CancellationToken cancellationToken)
        {
            if (!cmd.IsValid())
            {
                throw new Exception();
            }
            var source = _foodStoreEventSourceManager.ReplayEntity(cmd.Id);
            var newItem = FoodStoreDomain.Delete(source);
            var entity = _mapper.Map<FoodStore>(newItem);
            entity.Id = cmd.Id;
            _repo.Update(entity);
            if (_uow.Commit())
            {
                _bus.RaiseEvent(new StoreDeletedEvent(newItem.Name, newItem.Link, cmd.Id));

            }
            return Task.FromResult(new Unit());

        }
    }
}
