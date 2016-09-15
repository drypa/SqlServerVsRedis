using System;

namespace SqlServerVsRedis
{
    internal class SqlServerDataSaver<T> : IDataSaver<T>
        where T : class, new()
    {
        public void Save(T data, Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}