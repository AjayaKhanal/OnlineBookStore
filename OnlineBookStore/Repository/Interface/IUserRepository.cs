using OnlineBookStore.DTO;
using OnlineBookStore.Pages.User;
using System.Data;

namespace OnlineBookStore.Repository.Interface
{
    public interface IUserRepository
    {
        Task<(DataTable Users, string Message, int Code)> GetAllUserAsync();
        Task<(DataRow User, string Message, int Code)> GetUserByIdAsync(int id);
        Task<(string Message, int Code)> InsertUserAsync(UserDto UserModel);
        Task<(string Message, int Code)> UpdateUserAsync(int id, UserDto UserModel);
        Task<(string Message, int Code)> DeleteUserAsync(int id);
    }
}
