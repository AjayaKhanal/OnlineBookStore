using System.Data;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.Permission;
using OnlineBookStore.Repository.Interface;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Services.Implementation
{
    public class PermissionService: IPermissionServices
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            var (dataTable, message, code) = await _permissionRepository.GetAllPermissionsAsync();
            var permissions = dataTable.AsEnumerable()
                                   .Select(row => new PermissionDto
                                   {
                                       PermissionId = Convert.ToInt32(row["PermissionId"]),
                                       Name = row["Name"].ToString(),
                                       Module = row["Module"].ToString(),
                                       Action = row["Action"].ToString(),
                                       Resource = row["Resource"].ToString(),
                                       IsActive = Convert.ToBoolean(row["IsActive"]),
                                       Message = message,
                                       Code = code
                                   }).ToList();

            return permissions;
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int id)
        {
            var (row, message, code) = await _permissionRepository.GetPermissionByIdAsync(id);
            if (row == null)
            {
                return new PermissionDto { Message = message, Code = code };
            }

            return new PermissionDto
            {
                PermissionId = Convert.ToInt32(row["PermissionId"]),
                Name = row["Name"].ToString(),
                Module = row["Module"].ToString(),
                Action = row["Action"].ToString(),
                Resource = row["Resource"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                Message = message,
                Code = code
            };
        }

        public async Task<PermissionDto> InsertPermissionAsync(PermissionDto permissionDto)
        {
            var result = await _permissionRepository.InsertPermissionAsync(new PermissionDto
            {
                PermissionId = permissionDto.PermissionId,
                Name = permissionDto.Name,
                Module = permissionDto.Module,
                Action = permissionDto.Action,
                Resource = permissionDto.Resource,
                IsActive = permissionDto.IsActive
            });

            permissionDto.Message = result.Message;
            permissionDto.Code = result.Code;
            return permissionDto;
        }

        public async Task<PermissionDto> UpdatePermissionAsync(int id, PermissionDto permissionDto)
        {
            var result = await _permissionRepository.UpdatePermissionAsync(id, new PermissionDto
            {
                PermissionId = id,
                Name = permissionDto.Name,
                Module = permissionDto.Module,
                Action = permissionDto.Action,
                Resource = permissionDto.Resource,
                IsActive = permissionDto.IsActive
            });

            permissionDto.Message = result.Message;
            permissionDto.Code = result.Code;
            return permissionDto;
        }

        public async Task<PermissionDto> DeletePermissionAsync(int id)
        {
            var (message, code) = await _permissionRepository.DeletePermissionAsync(id);
            return new PermissionDto
            {
                PermissionId = id,
                Message = message,
                Code = code
            };
        }
    }
}
