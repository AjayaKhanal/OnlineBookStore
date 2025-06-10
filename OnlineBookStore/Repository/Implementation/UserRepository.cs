using System.Data;
using System.Data.SqlClient;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.User;
using OnlineBookStore.Repository.Interface;

namespace OnlineBookStore.Repository.Implementation
{
    public class UserRepository :IUserRepository
    {
        private readonly IBaseRepository _repository;

        public UserRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<(DataTable Users, string Message, int Code)> GetAllUserAsync()
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_USER_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECTALL");

            var(messageParam, codeParam) = _repository.AddCommonParameters(command);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return(dt, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(DataRow User, string Message, int Code)> GetUserByIdAsync(int id)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_USER_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECT");
            command.Parameters.AddWithValue("@USERID", id);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return (dt.Rows.Count > 0 ? dt.Rows[0] : null, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> InsertUserAsync(UserDto userDto)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_USER_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "INSERT");
            command.Parameters.AddWithValue("@USERID", userDto.UserId);
            command.Parameters.AddWithValue("@FULLNAME", userDto.Fullname);
            command.Parameters.AddWithValue("@EMAIL", userDto.Email);
            command.Parameters.AddWithValue("@MOBILE", userDto.Mobile);
            command.Parameters.AddWithValue("@PASSWORD", userDto.PasswordHash);
            command.Parameters.AddWithValue("@SALT", userDto.PasswordSalt);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> UpdateUserAsync(int id, UserDto userDto)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_USER_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "UPDATE");
            command.Parameters.AddWithValue("@USERID", userDto.UserId);
            command.Parameters.AddWithValue("@FULLNAME", userDto.Fullname);
            command.Parameters.AddWithValue("@EMAIL", userDto.Email);
            command.Parameters.AddWithValue("@MOBILE", userDto.Mobile); ;

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> UpdatePasswordAsync(int id, string password)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_USER_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "CHANGEPASSWORD");
            command.Parameters.AddWithValue("@PASSWORD", password);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> DeleteUserAsync(int id)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_USER_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "DELETE");
            command.Parameters.AddWithValue("@USERID", id);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

    }
}
