using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.DTO;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Pages.Author
{
    public class AuthorModel : PageModel
    {
        private readonly IAuthorServices _authorService;

        public AuthorModel(IAuthorServices authorService)
        {
            _authorService = authorService;
        }

        public List<AuthorItem> Authors { get; set; } = new List<AuthorItem>();
        [BindProperty]
        public AuthorItem Author { get; set; } = new AuthorItem();

        [TempData]
        public string? FeedBackMessage { get; set; }

        [TempData]
        public string? FeedbackType { get; set; }
        public async Task OnGet()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = authors.Select(a => new AuthorItem
            {
                AuthorId = a.AuthorId,
                AuthorName = a.AuthorName
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

            var dto = new AuthorDto
            {
                AuthorName = Author.AuthorName
            };

            var result = await _authorService.InsertAuthorAsync(dto);

            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var existingAuthor = await _authorService.GetAuthorByIdAsync(id);
            if (existingAuthor == null)
            {
                FeedBackMessage = "Author not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                FeedBackMessage = "Invalid input";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var dto = new AuthorDto
            {
                AuthorId = id,
                AuthorName = Author.AuthorName
            };

            var result = await _authorService.UpdateAuthorAsync(id, dto);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";
            return RedirectToPage();
        }

        public async Task<AuthorItem> Edit(int id)    
        {
            var edit = new AuthorItem();

            try
            {
                var existingAuthor = await _authorService.GetAuthorByIdAsync(id);

                if (existingAuthor != null)
                {
                    edit.AuthorId = existingAuthor.AuthorId;
                    edit.AuthorName = existingAuthor.AuthorName;
                }
                else
                {
                    edit.Code = "300";
                    edit.Message = "Author Not found";
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
            var author = await Edit(id); 
            return new JsonResult(author);
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var existingAuthor = await _authorService.GetAuthorByIdAsync(id);
            if (existingAuthor == null)
            {
                FeedBackMessage = "Author not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var result = await _authorService.DeleteAuthorAsync(id);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";
            return RedirectToPage();
        }
    }

    public class AuthorItem
    {
        public int? AuthorId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Author name is required.")]
        [StringLength(100)]
        public string AuthorName { get; set; }
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
