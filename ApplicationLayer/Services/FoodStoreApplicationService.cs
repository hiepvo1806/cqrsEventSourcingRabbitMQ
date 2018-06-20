using ApplicationLayer.ViewModel;
using AutoMapper;
using CommandStack.Commands;
using InstratructureLayer.Bus;
using InstratructureLayer.Entity;
using ReadStack;
using System;

namespace ApplicationLayer.Services
{
    public class FoodStoreApplicationService : BaseCRUDApplicationService<FoodStore, Guid, FoodStoreVM>, IFoodStoreApplicationService
    {
        public FoodStoreApplicationService(
            IReadService<FoodStore, Guid> readService,
            IMediatorHandler bus,
            IMapper mapper

            ) : base(readService,
                bus,mapper)
        {
        }

        public override void Create(FoodStoreVM Item)
        {
            var createCommand = _mapper.Map<CreateFoodStoreCommand>(Item);
            _bus.SendCommand(createCommand);
        }

        public override void Delete(Guid Id)
        {
            var deleteCommand = new DeleteFoodStoreCommand(Id);
            _bus.SendCommand(deleteCommand);
        }

        public override void Update(FoodStoreVM Item)
        {
            var updateCommand = _mapper.Map<UpdateFoodStoreCommand>(Item);
            _bus.SendCommand(updateCommand);
        }
    }

    public interface IFoodStoreApplicationService : IBaseCRUDApplicationService<FoodStore, Guid, FoodStoreVM>
    {

    }
}
