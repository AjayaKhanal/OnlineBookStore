using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineBookStore.Pages.Author
{
    public class AuthorModel : PageModel
    {
        public int AuthorId { get; set; }
        [BindProperty]
        public string AuthorName { get; set; }

        public List<string> Authors { get; set; } = new List<string>();
        public void OnGet()
        {
        }
    }
}
