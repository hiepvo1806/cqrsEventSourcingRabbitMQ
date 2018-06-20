using InstratructureLayer;
using InstratructureLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutoMapper;

namespace CommandStack
{
    public abstract class EventSourceManager<T, U> : IEventSourceManager<T,U> 
  
    {
        protected IRepository<EventEntity, Guid> _eventRepo;
        protected IMapper _mapper;

        public EventSourceManager(
            IRepository<EventEntity, Guid> eventRepo,
            IMapper mapper
            )
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        public void Log(EventEntity entity)
        {
            _eventRepo.Add(entity);
        }

        public abstract T ReplayEntity(U id);
    }
}
