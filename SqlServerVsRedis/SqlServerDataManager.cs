using System;

namespace SqlServerVsRedis
{
    internal sealed class SqlServerDataManager<T> : IDataManager<T>
        where T : class, new()
    {
        public void Save(T data, Guid id)
        {
            throw new NotImplementedException();
        }

        public T Load(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}