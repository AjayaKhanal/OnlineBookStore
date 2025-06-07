using System.Data.SqlClient;

namespace OnlineBookStore.Repository.Interface
{
    public interface IBaseRepository
    {
        SqlConnection CreateConnection();
        (SqlParameter MessageParam, SqlParameter CodeParam) AddCommonParameters(SqlCommand command);
    }
}
