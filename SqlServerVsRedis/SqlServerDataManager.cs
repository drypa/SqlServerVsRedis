using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerVsRedis
{
    internal sealed class SqlServerDataManager<T> : IDataManager<T>
        where T : class, new()
    {
        private const string SqlDeleteCommand = "DELETE FROM dbo.Data WHERE Id = @id";
        private const string SqlInsertCommand = "INSERT INTO dbo.Data (Id, Data) VALUES (@id, @data)";
        private const string SqlSelectCommand = "SELECT Data FROM dbo.Data WHERE Id = @id";

        private readonly ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["BlobTest"];

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SqlDeleteCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public T Load(Guid id)
        {
            var serializer = new DataSerializer();
            byte[] data;
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SqlSelectCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    data = command.ExecuteScalar() as byte[];
                }
            }
            return serializer.FromProto<T>(data);
        }

        public void Save(T data, Guid id)
        {
            var serializer = new DataSerializer();
            byte[] bytes = serializer.ToProto(data);
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SqlInsertCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlParameter dataParameter = command.Parameters.Add("@data", SqlDbType.VarBinary, bytes.Length);
                    dataParameter.Value = bytes;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
