using System.Data;
using System.Data.SqlClient;
using OnlineBookStore.Repository.Interface;
using OnlineBookStore.Pages.Category;

namespace OnlineBookStore.Repository.Implementation
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly IBaseRepository _baseRepository;

        public CategoryRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<(DataTable Categories, string Message, int Code)> GetAllCategoryAsync()
        {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_CATEGORY_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ACTION", "SELECTALL");

            var (messageParam, codeParam) = AddCommonParameters(command);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt= new DataTable();
            dt.Load(reader);

            return (dt, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(DataRow Category, string Message, int Code)> GetCategoryByIdAsync(int id)
        {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_CATEGORY_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ACTION", "SELECT");
            command.Parameters.AddWithValue("@CATEGORYID", id);

            var (messageParam, codeParam) = AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return (dt.Rows.Count > 0 ? dt.Rows[0] : null, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> InsertCategoryAsync(CategoryItem CategoryItem)
        {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_Category_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ACTION", "INSERT");
            command.Parameters.AddWithValue("@CATEGORYID", CategoryItem.CategoryId);
            command.Parameters.AddWithValue("@CATEGORYNAME", CategoryItem.CategoryName);

            var (messageParam, codeParam) = AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> UpdateCategoryAsync(int id, CategoryItem CategoryItem)
        {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_Category_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ACTION", "UPDATE");
            command.Parameters.AddWithValue("@CATEGORYID", CategoryItem.CategoryId);
            command.Parameters.AddWithValue("@CATEGORYNAME", CategoryItem.CategoryName);

            var (messageParam, codeParam) = AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> DeleteCategoryAsync(int id)
        {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_Category_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@ACTION", "DELETE");
            command.Parameters.AddWithValue("@CategoryID", id);

            var (messageParam, codeParam) = AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }
        private (SqlParameter MessageParam, SqlParameter CodeParam) AddCommonParameters(SqlCommand command)
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
