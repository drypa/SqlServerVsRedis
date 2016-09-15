using System;

namespace SqlServerVsRedis
{
    public interface IDataSaver<in T>
        where T : class, new()
    {
        void Save(T data, Guid id);
        void Delete(Guid id);
    }
}
