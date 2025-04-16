using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineBookStore.Pages.Author
{
    public class AuthorModel : PageModel
    {
        public List<AuthorItem> Authors { get; set; } = new List<AuthorItem>();
        public AuthorItem Author { get; set; } = new AuthorItem();
        public void OnGet()
        {
            Authors = new List<AuthorItem>
            {
                new AuthorItem { AuthorName = "Ajaya" },
                new AuthorItem { AuthorName = "Aj" },
                new AuthorItem { AuthorName = "Author" }
            };
        }
    }

    public class AuthorItem
    {
        public int AuthorId { get; set; }
        [BindProperty]
        public string AuthorName { get; set; }
    }
}
