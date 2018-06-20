using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.DomainModel
{
    //public abstract class BaseDomain<T> : IBaseDomain<T>
    //{
    //    //public T Id { get; set; }
    //    public abstract T Id { get; }
    //}

    public interface IBaseDomain<T>
    {
        T Id { get; }
    }
}
