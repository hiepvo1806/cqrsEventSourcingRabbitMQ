using InstratructureLayer.Events;
using System.Threading.Tasks;

namespace InstratructureLayer.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
