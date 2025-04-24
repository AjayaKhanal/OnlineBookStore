using OnlineBookStore.DTO;

namespace OnlineBookStore.Services.Interface
{
    public interface IAuthorServices
    {
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<AuthorDto> InsertAuthorAsync(AuthorDto authorDto);
        Task<AuthorDto> UpdateAuthorAsync(int id, AuthorDto authorDto);
        Task<AuthorDto> DeleteAuthorAsync(int id);
    }
}
