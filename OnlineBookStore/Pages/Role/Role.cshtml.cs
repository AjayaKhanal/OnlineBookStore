using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Pages.Book;

namespace OnlineBookStore.Pages.Role
{
    public class RoleModel : PageModel
    {
        public List<RoleItem> Roles { get; set; } = new List<RoleItem>();
        public RoleItem Role { get; set; } = new RoleItem();
        public void OnGet()
        {
            Roles = new List<RoleItem>
            {
                new RoleItem { RoleName = "Admin" },
                new RoleItem { RoleName = "User" },
                new RoleItem { RoleName = "Manager" }
            };
        }
    }

    public class RoleItem
    {
        public int RoleId { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
    }
}
