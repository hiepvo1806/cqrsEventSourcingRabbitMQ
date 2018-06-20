using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.EventSourcing.Bus
{
    public interface IUnitOfWork 
    {
        bool Commit();
    }
}
