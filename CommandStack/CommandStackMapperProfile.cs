using AutoMapper;
using CommandStack.FoodStoreEvent;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandStack
{
    public class CommandStackMapperProfile : Profile
    {
        public override string ProfileName => "CommandStackMapper Profile";
        public CommandStackMapperProfile()
        {
            CreateMap<FoodStoreDomain, FoodStore>();

            CreateMap<StoreCreatedEvent, EventEntity>()
              .ForMember(d => d.AggId, o => o.MapFrom(t => t.AggregateId))
              .AfterMap((s, d) => {
                  d.UpdatedDate = d.CreatedDate = DateTime.Now;
                  d.ObjType = typeof(FoodStore).ToString();
                  d.Type = EventType.Create;
                  d.JsonData = JsonConvert.SerializeObject(new { name = s.Name, link = s.Link });
              });

            CreateMap<StoreUpdatedEvent, EventEntity>()
              .ForMember(d => d.AggId, o => o.MapFrom(t => t.AggregateId))
              .AfterMap((s, d) => {
                  d.UpdatedDate = d.CreatedDate = DateTime.Now;
                  d.ObjType = typeof(FoodStore).ToString();
                  d.Type = EventType.Update;
                  d.JsonData = JsonConvert.SerializeObject(new { name = s.Name, link = s.Link });
              });

            CreateMap<StoreDeletedEvent, EventEntity>()
              .ForMember(d => d.AggId, o => o.MapFrom(t => t.AggregateId))
              .AfterMap((s, d) => {
                  d.UpdatedDate = d.CreatedDate = DateTime.Now;
                  d.ObjType = typeof(FoodStore).ToString();
                  d.Type = EventType.Delete;
                  d.JsonData = JsonConvert.SerializeObject(new { name = s.Name, link = s.Link });
              });
        }
    }

}
