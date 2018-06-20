using InstratructureLayer.Entity;

namespace CommandStack
{
    public interface IEventSourceManager<T,U>
    {
        void Log(EventEntity entity);
        T ReplayEntity(U id);
    }
}