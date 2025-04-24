using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Repository.Interface;

namespace OnlineBookStore.Pages.Author
{
    public class AuthorModel : PageModel
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<AuthorItem> Authors { get; set; } = new List<AuthorItem>();
        public AuthorItem Author { get; set; } = new AuthorItem();
        public async Task OnGet()
        {
            var (dt, message, code) = await _authorRepository.GetAllAuthorsAsync();
            Authors = dt.AsEnumerable().Select(row => new AuthorItem
            {
                AuthorId = row.Field<int>("AuthorId"),
                AuthorName = row.Field<string>("AuthorName")
            }).ToList();

        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                Author.FeedBackMessage = "Invalid input";
                Author.FeedbackType = "danger";
                return Page();
            }

            var (message, code) = await _authorRepository.InsertAuthorAsync(Author);
            Author.FeedBackMessage = message;
            Author.FeedbackType = code == 200 ? "success" : "danger";
            return RedirectToPage();
        }
    }

    public class AuthorItem
    {
        public int AuthorId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Author name is required.")]
        [StringLength(100)]
        public string AuthorName { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        [TempData]
        public string FeedBackMessage { get; set; }

        [TempData]
        public string FeedbackType { get; set; }
    }
}
