using System;
using System.IO;
using ProtoBuf;

namespace SqlServerVsRedis
{
    public class DataSerializer
    {
        public TData FromProto<TData>(byte[] value)
        {
            using (var stream = new MemoryStream(value))
            {
                var item = Serializer.Deserialize<TData>(stream);
                return item;
            }
        }

        public TData FromProtoStream<TData>(Stream stream)
        {
            var item = Serializer.Deserialize<TData>(stream);
            return item;
        }

        public byte[] ToProto<T>(T value)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, value);
                return stream.ToArray();
            }
        }

        public Stream ToProtoStream<T>(T value)
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, value);
            return stream;
        }
    }
}
