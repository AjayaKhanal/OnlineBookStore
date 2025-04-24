using System.Data;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.Author;
using OnlineBookStore.Repository.Implementation;
using OnlineBookStore.Repository.Interface;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Services.Implementation
{
    public class AuthorServices:IAuthorServices
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorServices(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            var (dataTable, message, code) = await _authorRepository.GetAllAuthorsAsync();
            var authors = dataTable.AsEnumerable()
                                   .Select(row => new AuthorDto
                                   {
                                       AuthorId = Convert.ToInt32(row["AuthorId"]),
                                       AuthorName = row["AuthorName"].ToString(),
                                       Message = message,
                                       Code = code
                                   }).ToList();

            return authors;
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var (row, message, code) = await _authorRepository.GetAuthorByIdAsync(id);
            if (row == null)
            {
                return new AuthorDto { Message = message, Code = code };
            }

            return new AuthorDto
            {
                AuthorId = Convert.ToInt32(row["AuthorId"]),
                AuthorName = row["AuthorName"].ToString(),
                Message = message,
                Code = code
            };
        }

        public async Task<AuthorDto> InsertAuthorAsync(AuthorDto authorDto)
        {
            var result = await _authorRepository.InsertAuthorAsync(new AuthorItem
            {
                AuthorId = authorDto.AuthorId,
                AuthorName = authorDto.AuthorName
            });

            authorDto.Message = result.Message;
            authorDto.Code = result.Code;
            return authorDto;
        }

        public async Task<AuthorDto> UpdateAuthorAsync(int id, AuthorDto authorDto)
        {
            var result = await _authorRepository.UpdateAuthorAsync(id, new AuthorItem
            {
                AuthorId = id,
                AuthorName = authorDto.AuthorName
            });

            authorDto.Message = result.Message;
            authorDto.Code = result.Code;
            return authorDto;
        }

        public async Task<AuthorDto> DeleteAuthorAsync(int id)
        {
            var (message, code) = await _authorRepository.DeleteAuthorAsync(id);
            return new AuthorDto
            {
                AuthorId = id,
                Message = message,
                Code = code
            };
        }
    }
}
