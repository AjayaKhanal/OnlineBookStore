using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.Book;
using OnlineBookStore.Services.Implementation;
using OnlineBookStore.Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineBookStore.Pages.Role
{
    public class RoleModel : PageModel
    {
        private readonly IRoleServices _roleServices;

        public RoleModel(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        public List<RoleItem> Roles { get; set; } = new List<RoleItem>();
        [BindProperty]
        public RoleItem Role { get; set; } = new RoleItem();
        [TempData]
        public string? FeedBackMessage { get; set; }

        [TempData]
        public string? FeedbackType { get; set; }

        public async Task OnGet()
        {
            var roles = await _roleServices.GetAllRolesAsync();
            Roles = roles.Select(r => new RoleItem
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            foreach(var value in ModelState.Values)
            {
                foreach(var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid)
            {
                FeedBackMessage = "Invalid input";
                FeedbackType = "danger";
            }

            var dto = new RoleDto
            {
                RoleName = Role.RoleName
            };

            var result = await _roleServices.InsertRoleAsync(dto);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _roleServices.DeleteRoleAsync(id);
            return RedirectToPage();
        }
    }

    public class RoleItem
    {
        public int RoleId { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="Role name is Required")]
        [StringLength(100)]
        public string RoleName { get; set; }
    }
}
