using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.MessageService
{
    public interface IMessageNotify<T>
    {
        //void SetUpService();
        void NotifyService(T content);
    }
}
