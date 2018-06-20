using AutoMapper;
using InstratructureLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstratructureLayer
{
    public interface IRepository<T,U> where T: BaseEntity<U>
    {
        IQueryable<T> GetAll();
        T Get(U Id);
        U Add(T Item);
        void Delete(U Id);
        void Update(T Item);
        //int Commit();

        
    }

    public class Repository<T, U> : IRepository<T, U> where T:BaseEntity<U>
    {
        protected readonly DbContext _context;
        private readonly IMapper _mapper;

        protected DbSet<T> EntitySet => _context.Set<T>();
        protected virtual IQueryable<T> Entities => EntitySet.AsQueryable();

        

        public Repository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual U Add(T Item)
        {
            var addedItem =  EntitySet.Add(Item);
            return addedItem.Entity.Id;
        }

        public virtual void Delete(U Id)
        {
            var foundEntity = Get(Id);
            if (foundEntity == null)
                throw new Exception("Entity not found");
            _context.Entry(foundEntity).State = EntityState.Deleted;
        }

        public virtual T Get(U Id)
        {
            return Entities.FirstOrDefault(q=> EqualityComparer<U>.Default.Equals(q.Id,Id));

        }

        public virtual IQueryable<T> GetAll()
        {
            return Entities;
        }

        public virtual void Update(T Item)
        {
            throw new NotImplementedException();
        }
    }
}
