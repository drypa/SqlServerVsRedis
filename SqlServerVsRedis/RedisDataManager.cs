using System;
using StackExchange.Redis;

namespace SqlServerVsRedis
{
    internal sealed class RedisDataManager : IDataManager<byte[]>
    {
        public void Save(byte[] data, Guid id)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost"))
            {

                IDatabase db = redis.GetDatabase();
                db.StringSet(id.ToString(), data);
            }
        }

        public byte[] Load(Guid id)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost"))
            {
                IDatabase db = redis.GetDatabase();
                return db.StringGet(id.ToString());
            }
        }

        public void Delete(Guid id)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost"))
            {
                IDatabase db = redis.GetDatabase();
                db.KeyDelete(id.ToString());
            }
        }

        public string Type => "Redis";
    }
}