using System.Data;
using System.Data.SqlClient;
using OnlineBookStore.Pages.Role;
using OnlineBookStore.Repository.Interface;

namespace OnlineBookStore.Repository.Implementation
{
    public class RoleRepository: IRoleRepository
    {
        private readonly IBaseRepository _baseRepository;

        public RoleRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<(DataTable Roles, string Message, int Code)> GetAllRolesAsync() {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_ROLES_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECTALL");

            var (messageParam, codeParam) = _baseRepository.AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);
            return (dt, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(DataRow Role, string Message, int Code)> GetRoleByIdAsync(int id) {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_ROLES_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "SELECT");
            command.Parameters.AddWithValue("@ROLEID", id);

            var (messageParam, codeParam) = _baseRepository.AddCommonParameters(command);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            return (dt.Rows.Count > 0 ? dt.Rows[0] : null, messageParam.Value.ToString(), (int)codeParam.Value);
        }

        public async Task<(string Message, int Code)> InsertRoleAsync(RoleItem roleItem) {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_ROLES_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "INSERT");
            command.Parameters.AddWithValue("@ROLEID", roleItem.RoleId);
            command.Parameters.AddWithValue("@ROLENAME", roleItem.RoleName);

            var (messageParam, codeParam) = _baseRepository.AddCommonParameters(command);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }
        public async Task<(string Message, int Code)> UpdateRoleAsync(int id, RoleItem roleItem) {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_ROLES_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "UPDATE");
            command.Parameters.AddWithValue("@ROLEID", roleItem.RoleId);
            command.Parameters.AddWithValue("@ROLENAME", roleItem.RoleName);

            var (messageParam, codeParam) = _baseRepository.AddCommonParameters(command);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }
        public async Task<(string Message, int Code)> DeleteRoleAsync(int id) {
            var connection = _baseRepository.CreateConnection();
            var command = new SqlCommand("SP_ROLES_CRUD", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ACTION", "DELETE");
            command.Parameters.AddWithValue("@ROLEID", id);

            var (messageParam, codeParam) = _baseRepository.AddCommonParameters(command);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (messageParam.Value.ToString(), (int)codeParam.Value);
        }

    }
}
