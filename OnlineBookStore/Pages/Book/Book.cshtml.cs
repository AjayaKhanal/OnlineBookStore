using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineBookStore.Pages.Book
{
    public class BookModel : PageModel
    {
        public int BookId { get; set; }
        [BindProperty]
        public string BookTitle { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        //public string Author { get; set; }
        public List<String> authors { get; set; } = new List<string>();

        public List<BookModel> Books { get; set; } = new List<BookModel>();
        public void OnGet()
        {
        }
    }
}
