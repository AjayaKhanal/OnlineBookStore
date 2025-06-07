using System.Data;
using OnlineBookStore.DTO;

namespace OnlineBookStore.Repository.Interface
{
    public interface IPermissionRepository
    {
        Task<(DataTable Permission, string Message, int Code)> GetAllPermissionsAsync();
        Task<(DataRow Permission, string Message, int Code)> GetPermissionByIdAsync(int id);
        Task<(string Message, int Code)> InsertPermissionAsync(PermissionDto authorModel);
        Task<(string Message, int Code)> UpdatePermissionAsync(int id, PermissionDto authorModel);
        Task<(string Message, int Code)> DeletePermissionAsync(int id);
    }
}
