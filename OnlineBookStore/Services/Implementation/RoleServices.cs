using System.Data;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.Role;
using OnlineBookStore.Repository.Interface;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Services.Implementation
{
    public class RoleServices: IRoleServices
   {
        private readonly IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync() {
            var (dataTable, message, code) = await _roleRepository.GetAllRolesAsync();
            var roles = dataTable.AsEnumerable()
                .Select(row => new RoleDto
                {
                    RoleId = Convert.ToInt32(row["RoleId"]),
                    RoleName = row["RoleName"].ToString(),
                    Message = message,
                    Code = code
                }).ToList();
            return roles;
        }
        
        public async Task<RoleDto> GetRoleByIdAsync(int id) {
            var (row, message, code) = await _roleRepository.GetRoleByIdAsync(id);
            if (row == null)
            {
                return new RoleDto { Message = message, Code = code };
            }

            return new RoleDto
            {
                RoleId = Convert.ToInt32(row["RoleId"]),
                RoleName = row["RoleName"].ToString(),
                Message = message,
                Code = code
            };
        }
        public async Task<RoleDto> InsertRoleAsync(RoleDto roleDto) {
            var result = await _roleRepository.InsertRoleAsync(new RoleItem
            {
                RoleId = roleDto.RoleId,
                RoleName = roleDto.RoleName
            });

            roleDto.Message = result.Message;
            roleDto.Code = result.Code;
            return(roleDto);
        }
        public async Task<RoleDto> UpdateRoleAsync(int id, RoleDto roleDto) {
            var result = await _roleRepository.UpdateRoleAsync(id, new RoleItem
            {
                RoleId = id,
                RoleName = roleDto.RoleName
            });

            roleDto.Message = result.Message;
            roleDto.Code = result.Code;
            return (roleDto);
        }
        public async Task<RoleDto> DeleteRoleAsync(int id) {
            var (message, code) = await _roleRepository.DeleteRoleAsync(id);
            return new RoleDto
            {
                RoleId = id,
                Message = message,
                Code = code
            };
        }
    }
}
