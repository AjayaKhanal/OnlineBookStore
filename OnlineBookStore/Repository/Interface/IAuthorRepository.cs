using OnlineBookStore.Pages.Author;

namespace OnlineBookStore.Repository.Interface
{
    public interface IAuthorRepository
    {
        public void GetAllAuthors();
        public void GetAuthorById(int id);
        public void InsertAuthor(AuthorModel authorModel);
        public void UpdateAuthor(int id, AuthorModel authorModel);
        public void DeleteAuthor(int id);
    }
}
