using System.Data;
using System.Data.SqlClient;
using OnlineBookStore.Pages.Author;
using OnlineBookStore.Repository.Interface;

namespace OnlineBookStore.Repository.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IBaseRepository _repository;

        public AuthorRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<(DataTable Authors, string Message, int Code)> GetAllAuthorsAsync()
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_AUTHOR_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECTALL");

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return (dt, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(DataRow Author, string Message, int Code)> GetAuthorByIdAsync(int id)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_AUTHOR_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECT");
            command.Parameters.AddWithValue("@AUTHORID", id);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return (dt.Rows.Count > 0 ? dt.Rows[0] : null, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> InsertAuthorAsync(AuthorItem authorItem)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_AUTHOR_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "INSERT");
            command.Parameters.AddWithValue("@AUTHORID", authorItem.AuthorId);
            command.Parameters.AddWithValue("@AUTHORNAME", authorItem.AuthorName);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> UpdateAuthorAsync(int id, AuthorItem authorItem)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_AUTHOR_CRUD", connection) 
            { 
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "UPDATE");
            command.Parameters.AddWithValue("@AUTHORID", authorItem.AuthorId);
            command.Parameters.AddWithValue("@AUTHORNAME", authorItem.AuthorName);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return(messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> DeleteAuthorAsync(int id)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_AUTHOR_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "DELETE");
            command.Parameters.AddWithValue("@AUTHORID", id);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        
    }
}
