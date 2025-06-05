using System.Data;
using OnlineBookStore.DTO;
using OnlineBookStore.Repository.Interface;
using OnlineBookStore.Pages.Category;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Services.Implementation
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllCategoryAsync()
        {
            var (dataTable, message, code) = await _CategoryRepository.GetAllCategoryAsync();
            var Categorys = dataTable.AsEnumerable()
                                   .Select(row => new CategoryDto
                                   {
                                       CategoryId = Convert.ToInt32(row["CategoryId"]),
                                       CategoryName = row["CategoryName"].ToString(),
                                       Message = message,
                                       Code = code
                                   }).ToList();

            return Categorys;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var (row, message, code) = await _CategoryRepository.GetCategoryByIdAsync(id);
            if (row == null)
            {
                return new CategoryDto { Message = message, Code = code };
            }

            return new CategoryDto
            {
                CategoryId = Convert.ToInt32(row["CategoryId"]),
                CategoryName = row["CategoryName"].ToString(),
                Message = message,
                Code = code
            };
        }

        public async Task<CategoryDto> InsertCategoryAsync(CategoryDto CategoryDto)
        {
            var result = await _CategoryRepository.InsertCategoryAsync(new CategoryItem
            {
                CategoryId = CategoryDto.CategoryId,
                CategoryName = CategoryDto.CategoryName
            });

            CategoryDto.Message = result.Message;
            CategoryDto.Code = result.Code;
            return CategoryDto;
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, CategoryDto CategoryDto)
        {
            var result = await _CategoryRepository.UpdateCategoryAsync(id, new CategoryItem
            {
                CategoryId = id,
                CategoryName = CategoryDto.CategoryName
            });

            CategoryDto.Message = result.Message;
            CategoryDto.Code = result.Code;
            return CategoryDto;
        }

        public async Task<CategoryDto> DeleteCategoryAsync(int id)
        {
            var (message, code) = await _CategoryRepository.DeleteCategoryAsync(id);
            return new CategoryDto
            {
                CategoryId = id,
                Message = message,
                Code = code
            };
        }
    }
}
