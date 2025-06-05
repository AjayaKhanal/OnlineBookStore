using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.DTO;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Pages.Category
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryService _CategoryService;

        public CategoryModel(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        public List<CategoryItem> Categories { get; set; } = new List<CategoryItem>();
        [BindProperty]
        public CategoryItem Category { get; set; } = new CategoryItem();

        [TempData]
        public string? FeedBackMessage { get; set; }
        [TempData]
        public string? FeedbackType { get; set; }

        public async Task OnGet()
        {
            var category = await _CategoryService.GetAllCategoryAsync();
            Categories = category.Select(a => new CategoryItem
            {
                CategoryId = a.CategoryId,
                CategoryName = a.CategoryName
            }).ToList();

        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid)
            {
                FeedBackMessage = "Invalid input";
                FeedbackType = "danger";
                //return Page();
            }

            var dto = new CategoryDto
            {
                CategoryName = Category.CategoryName
            };

            var result = await _CategoryService.InsertCategoryAsync(dto);

            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var existingCategory = await _CategoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                FeedBackMessage = "Category not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                FeedBackMessage = "Invalid input";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var dto = new CategoryDto
            {
                CategoryId = id,
                CategoryName = Category.CategoryName
            };

            var result = await _CategoryService.UpdateCategoryAsync(id, dto);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";
            return RedirectToPage();
        }

        public async Task<CategoryItem> Edit(int id)
        {
            var edit = new CategoryItem();

            try
            {
                var existingCategory = await _CategoryService.GetCategoryByIdAsync(id);

                if (existingCategory != null)
                {
                    edit.CategoryId = existingCategory.CategoryId;
                    edit.CategoryName = existingCategory.CategoryName;
                }
                else
                {
                    edit.Code = "300";
                    edit.Message = "Category Not found";
                }
            }
            catch (Exception ex)
            {
                edit.Code = "500";
                edit.Message = "Exception: " + ex.Message;
            }

            return edit;
        }


        public async Task<JsonResult> OnGetEditAsync(int id)
        {
            var Category = await Edit(id);
            return new JsonResult(Category);
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var existingCategory = await _CategoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                FeedBackMessage = "Category not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var result = await _CategoryService.DeleteCategoryAsync(id);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";
            return RedirectToPage();
        }
    }

    public class CategoryItem
    {
        public int CategoryId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100)]
        public string CategoryName { get; set; }
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
