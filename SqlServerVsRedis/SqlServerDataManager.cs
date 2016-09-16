using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerVsRedis
{
    internal sealed class SqlServerDataManager : IDataManager<byte[]>
    {
        private const string SqlDeleteCommand = "DELETE FROM dbo.Datas WHERE Id = @id";
        private const string SqlInsertCommand = "INSERT INTO dbo.Datas (Id, Data) VALUES (@id, @data)";
        private const string SqlSelectCommand = "SELECT Data FROM dbo.Datas WHERE Id = @id";

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

        public string Type => "SQL";

        public byte[] Load(Guid id)
        {
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
            return data;
        }

        public void Save(byte[] data, Guid id)
        {
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SqlInsertCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlParameter dataParameter = command.Parameters.Add("@data", SqlDbType.VarBinary, data.Length);
                    dataParameter.Value = data;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
