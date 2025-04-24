using System.Data;
using OnlineBookStore.Pages.Author;

namespace OnlineBookStore.Repository.Interface
{
    public interface IAuthorRepository
    {
        Task<(DataTable Authors, string Message, int Code)> GetAllAuthorsAsync();
        Task<(DataRow Author, string Message, int Code)> GetAuthorByIdAsync(int id);
        Task<(string Message, int Code)> InsertAuthorAsync(AuthorItem authorModel);
        Task<(string Message, int Code)> UpdateAuthorAsync(int id, AuthorItem authorModel);
        Task<(string Message, int Code)> DeleteAuthorAsync(int id);
    }
}
