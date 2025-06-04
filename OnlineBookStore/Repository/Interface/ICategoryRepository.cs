using System.Data;
using OnlineCategoryStore.Pages.Category;

namespace OnlineBookStore.Repository.Interface
{
    public interface ICategoryRepository: IBaseRepository
    {
        Task<(DataTable Categories, string Message, int Code)> GetAllCategorysAsync();
        Task<(DataRow Category, string Message, int Code)> GetCategoryByIdAsync(int id);
        Task<(string Message, int Code)> InsertCategoryAsync(CategoryItem CategoryModel);
        Task<(string Message, int Code)> UpdateCategoryAsync(int id, CategoryItem CategoryModel);
        Task<(string Message, int Code)> DeleteCategoryAsync(int id);
    }
}
