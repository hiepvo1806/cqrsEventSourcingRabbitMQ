using InstratructureLayer;
using InstratructureLayer.DomainModel;
using InstratructureLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IBaseCRUDApplicationService<T, U, D> where T: BaseEntity<U>
        //where D : IBaseDomain<U>
    {
        IEnumerable<T> GetAll();
        void Update(D Item);
        void Create(D Item);
        void Delete(U Id);
        T Get(U Id);
    }

    
}