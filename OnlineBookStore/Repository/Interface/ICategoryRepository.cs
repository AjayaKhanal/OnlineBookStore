using System.Data;
using OnlineBookStore.Pages.Category;

namespace OnlineBookStore.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<(DataTable Categories, string Message, int Code)> GetAllCategoryAsync();
        Task<(DataRow Category, string Message, int Code)> GetCategoryByIdAsync(int id);
        Task<(string Message, int Code)> InsertCategoryAsync(CategoryItem CategoryModel);
        Task<(string Message, int Code)> UpdateCategoryAsync(int id, CategoryItem CategoryModel);
        Task<(string Message, int Code)> DeleteCategoryAsync(int id);
    }
}
