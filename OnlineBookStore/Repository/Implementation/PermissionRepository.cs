using System.Data;
using System.Data.SqlClient;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.Author;
using OnlineBookStore.Repository.Interface;

namespace OnlineBookStore.Repository.Implementation
{
    public class PermissionRepository: IPermissionRepository
    {
        public readonly IBaseRepository _repository;

        public PermissionRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<(string Message, int Code)> DeletePermissionAsync(int id)
        {
            var connection =_repository.CreateConnection();
            var command = new SqlCommand("SP_PERMISSION_CRUD", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ACTION", "DELETE");
            command.Parameters.AddWithValue("@PERMISSIONID", id);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(DataTable Permission, string Message, int Code)> GetAllPermissionsAsync()
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_PERMISSION_CRUD", connection)
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

        public async Task<(DataRow Permission, string Message, int Code)> GetPermissionByIdAsync(int id)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_PERMISSION_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECT");
            command.Parameters.AddWithValue("@PERMISSIONID", id);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return (dt.Rows.Count > 0 ? dt.Rows[0] : null, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> InsertPermissionAsync(PermissionDto permissionDto)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_PERMISSION_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "INSERT");
            command.Parameters.AddWithValue("@PERMISSIONID", permissionDto.PermissionId);
            command.Parameters.AddWithValue("@PERMISSIONNAME", permissionDto.Name);
            command.Parameters.AddWithValue("@MODULE", permissionDto.Module);
            command.Parameters.AddWithValue("@P_ACTION", permissionDto.Action);
            command.Parameters.AddWithValue("@RESOURCE", permissionDto.Resource);
            command.Parameters.AddWithValue("@ISACTIVE", permissionDto.IsActive);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> UpdatePermissionAsync(int id, PermissionDto permissionDto)
        {
            var connection = _repository.CreateConnection();
            var command = new SqlCommand("SP_PERMISSION_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "UPDATE");
            command.Parameters.AddWithValue("@PERMISSIONID", permissionDto.PermissionId);
            command.Parameters.AddWithValue("@PERMISSIONNAME", permissionDto.Name);
            command.Parameters.AddWithValue("@MODULE", permissionDto.Module);
            command.Parameters.AddWithValue("@P_ACTION", permissionDto.Action);
            command.Parameters.AddWithValue("@RESOURCE", permissionDto.Resource);
            command.Parameters.AddWithValue("@ISACTIVE", permissionDto.IsActive);

            var (messageParam, codeParam) = _repository.AddCommonParameters(command);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }
    }
}
