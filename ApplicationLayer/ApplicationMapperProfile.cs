using ApplicationLayer.ViewModel;
using AutoMapper;
using CommandStack.Commands;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;

namespace ApplicationLayer
{
    public class ApplicationMapperProfile : Profile
    {
        public override string ProfileName => "Application Profile";
        public ApplicationMapperProfile()
        {
            CreateMap<FoodStore, FoodStoreVM>();
            CreateMap<FoodStoreVM, FoodStore>();
            CreateMap<FoodStoreVM, FoodStoreDomain>();

            CreateMap<FoodStoreVM,CreateFoodStoreCommand>()
                .ConstructUsing(c => new CreateFoodStoreCommand(c.Name, c.Link));

           
            CreateMap<FoodStoreVM, UpdateFoodStoreCommand>()
               .ConstructUsing(c => new UpdateFoodStoreCommand(c.Name, c.Link,c.Id));
        }
    }
}
