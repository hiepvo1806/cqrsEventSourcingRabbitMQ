using AutoMapper;
using CommandStack;
using InstratructureLayer;
using InstratructureLayer.Bus;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;
using ReadStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public abstract class BaseCRUDApplicationService<T, U , D> : IBaseCRUDApplicationService<T, U , D> where T : BaseEntity<U>
    {
        protected IReadService<T, U> _readService;
        protected IMediatorHandler _bus;
        protected IMapper _mapper;

        public BaseCRUDApplicationService(
            IReadService<T, U> readService, 
            IMediatorHandler bus,
            IMapper mapper
            )
        {
            _readService = readService;
            _bus = bus;
            _mapper = mapper;
        }
        public abstract void Create(D Item);

        public abstract void Delete(U Id);

        public T Get(U Id)
        {
            return _readService.Get(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _readService.GetAll();
        }

        public abstract void Update(D Item);
    }
}
