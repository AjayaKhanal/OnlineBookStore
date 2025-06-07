using OnlineBookStore.DTO;

namespace OnlineBookStore.Services.Interface
{
    public interface IPermissionServices
    {
        Task<List<PermissionDto>> GetAllPermissionsAsync();
        Task<PermissionDto> GetPermissionByIdAsync(int id);
        Task<PermissionDto> InsertPermissionAsync(PermissionDto authorDto);
        Task<PermissionDto> UpdatePermissionAsync(int id, PermissionDto authorDto);
        Task<PermissionDto> DeletePermissionAsync(int id);
    }
}
