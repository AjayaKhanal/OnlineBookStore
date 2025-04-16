using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineBookStore.Pages.Book
{
    public class BookModel : PageModel
    {
        public List<BookItem> Books { get; set; } = new List<BookItem>();
        public BookItem Book { get; set; } = new BookItem();

        public void OnGet()
        {
            Books = new List<BookItem>
    {
        new BookItem()
        {
            BookTitle = "Dotnet",
            Description = "Learn Dotnet",
            Rate = 400,
            Quantity = 10,
            Category = "IT"
        },
        new BookItem()
        {
            BookTitle = "Cybersecurity",
            Description = "Learn cyber security",
            Rate = 400,
            Quantity = 10,
            Category = "IT"
        },
        new BookItem()
        {
            BookTitle = "Networking",
            Description = "Learn networking",
            Rate = 400,
            Quantity = 10,
            Category = "IT"
        }
    };
        }

    }

    public class BookItem
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
    }
}
