using System;

namespace SqlServerVsRedis
{
    public interface IDataManager<T>
    {
        void Save(T data, Guid id);
        T Load(Guid id);
        void Delete(Guid id);
    }
}
