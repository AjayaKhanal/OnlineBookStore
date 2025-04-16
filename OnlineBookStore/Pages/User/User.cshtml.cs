using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineBookStore.Pages.User
{
    public class UserModel : PageModel
    {
        public List<UserItem> Users { get; set; } = new List<UserItem>();
        public UserItem User { get; set; } = new UserItem();
        public void OnGet()
        {
            Users = new List<UserItem>
            {
                new UserItem {
                    FullName = "Ajaya",
                    Email="ajaya@gmail.com"
                },
                new UserItem { 
                    FullName = "Ajay",
                    Email="ajay@gmail.com" 
                },
                new UserItem { 
                    FullName = "User",
                    Email="user@gmail.com" 
                }
            };
        }
    }

    public class UserItem
    {
        public int UserId { get; set; }
        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
    }
}
