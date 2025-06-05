using OnlineBookStore.DTO;

namespace OnlineBookStore.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> InsertCategoryAsync(CategoryDto CategoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, CategoryDto CategoryDto);
        Task<CategoryDto> DeleteCategoryAsync(int id);
    }
}
