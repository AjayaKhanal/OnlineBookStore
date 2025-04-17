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
    }
}
