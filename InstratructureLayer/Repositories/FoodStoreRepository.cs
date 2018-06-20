using System;
using System.Linq;
using AutoMapper;
using InstratructureLayer.Entity;
using Microsoft.EntityFrameworkCore;
namespace InstratructureLayer.Repositories
{
    public class FoodStoreRepository : Repository<FoodStore, Guid> , IFoodStoreRepository
    {
        public FoodStoreRepository(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override void Update(FoodStore Item)
        {
            var foundItem = Entities.First(q => q.Id == Item.Id);
            foundItem.IsDeleted = Item.IsDeleted;
            foundItem.Name = Item.Name;
            foundItem.Link = Item.Link;
        }
    }

    public interface IFoodStoreRepository: IRepository<FoodStore, Guid>
    {
    }
}
