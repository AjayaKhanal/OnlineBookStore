using System.Data;
using System.Data.SqlClient;
using OnlineBookStore.Repository.Interface;

namespace OnlineBookStore.Repository.Implementation
{
    public class BaseRepository: IBaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbConnection");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public (SqlParameter MessageParam, SqlParameter CodeParam) AddCommonParameters(SqlCommand command)
        {
            var messageParam = new SqlParameter("@MESSAGE", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            var codeParam = new SqlParameter("@CODE", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(messageParam);
            command.Parameters.Add(codeParam);

            return (messageParam, codeParam);
        }
    }
}
