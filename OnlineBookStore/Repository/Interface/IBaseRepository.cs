using System.Data.SqlClient;

namespace OnlineBookStore.Repository.Interface
{
    public interface IBaseRepository
    {
        SqlConnection CreateConnection();
    }
}
