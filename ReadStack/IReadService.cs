using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadStack
{
    public interface IReadService <T,U>
    {
        T Get(U Id);
        IEnumerable<T> GetAll();
    }
}
