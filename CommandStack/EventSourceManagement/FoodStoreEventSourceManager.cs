using AutoMapper;
using InstratructureLayer;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;
using InstratructureLayer.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandStack.EventSourceManagement
{
    public class FoodStoreEventSourceManager : EventSourceManager<FoodStoreDomain, Guid>, IFoodStoreEventSourceManager
    {
        public FoodStoreEventSourceManager(IEventRepository eventRepo, IMapper mapper) : base(eventRepo, mapper)
        {
        }

        public override FoodStoreDomain ReplayEntity(Guid id)
        {
            {
                FoodStoreDomain foundEntity = FoodStoreDomain.Create(string.Empty,string.Empty);
                var allEvent = _eventRepo.GetAll().Where(q => q.AggId == id.ToString()).OrderBy(o => o.CreatedDate);
                foreach (var action in allEvent)
                {
                    dynamic data = JsonConvert.DeserializeObject(action.JsonData);
                    switch (action.Type)
                    {
                        case EventType.Create:
                            try {
                                foundEntity = FoodStoreDomain.Create(data.name.Value, data.link.Value);
                            }
                            catch (Exception e)
                            {
                                var k = e;
                            }
                            break;
                        case EventType.Update:
                            foundEntity = FoodStoreDomain.Update(foundEntity,data.name.Value, data.link.Value);
                            
                            break;
                        case EventType.Delete:
                            foundEntity = FoodStoreDomain.Delete(foundEntity);
                            break;
                    }
                }
                return foundEntity;
            }
        }
    }



    public interface IFoodStoreEventSourceManager :  IEventSourceManager<FoodStoreDomain, Guid>
    {

    }
}
