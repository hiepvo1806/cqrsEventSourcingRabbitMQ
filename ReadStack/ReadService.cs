using InstratructureLayer;
using InstratructureLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadStack
{
    public class BaseReadService<T, U> : IReadService<T, U> where T: BaseEntity<U>
    {
        private readonly IRepository<T, U> _repo;
        public BaseReadService(IRepository<T, U> repo)
        {
            _repo = repo;
        }
        public T Get(U Id)
        {
            return _repo.Get(Id);
        }
        public IEnumerable<T> GetAll()
        {
            return _repo.GetAll();
        }

    }
}
