using OnlineBookStore.DTO;

namespace OnlineBookStore.Services.Interface
{
    public interface IRoleServices
    {
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int id);
        Task<RoleDto> InsertRoleAsync(RoleDto roleDto);
        Task<RoleDto> UpdateRoleAsync(int id, RoleDto roleDto);
        Task<RoleDto> DeleteRoleAsync(int id);
    }
}
