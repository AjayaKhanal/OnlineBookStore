using System.Data;
using OnlineBookStore.Pages.Role;

namespace OnlineBookStore.Repository.Interface
{
    public interface IRoleRepository
    {
        Task<(DataTable Roles, string Message, int Code)> GetAllRolesAsync();
        Task<(DataRow Role, string Message, int Code)> GetRoleByIdAsync(int id);
        Task<(string Message, int Code)> InsertRoleAsync(RoleItem roleItem);
        Task<(string Message, int Code)> UpdateRoleAsync(int id, RoleItem RoleItem);
        Task<(string Message, int Code)> DeleteRoleAsync(int id);
    }
}
