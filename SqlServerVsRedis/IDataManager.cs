using System;

namespace SqlServerVsRedis
{
    public interface IDataManager<T>
        where T : class, new()
    {
        void Save(T data, Guid id);
        T Load(Guid id);
        void Delete(Guid id);
    }
}
