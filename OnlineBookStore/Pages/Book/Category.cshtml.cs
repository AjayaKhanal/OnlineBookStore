using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Services.Interface;

namespace OnlineCategoryStore.Pages.Category
{
    public class CategoryModel : PageModel
    {
        //private readonly ICategoryServices _CategoryService;

        //public CategoryModel(ICategoryServices CategoryService)
        //{
        //    _CategoryService = CategoryService;
        //}

        public List<CategoryItem> Categories { get; set; } = new List<CategoryItem>();
        [BindProperty]
        public CategoryItem Category { get; set; } = new CategoryItem();

        [TempData]
        public string? FeedBackMessage { get; set; }

        [TempData]
        public string? FeedbackType { get; set; }

        public void OnGet()
        {
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
