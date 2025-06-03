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

        public async Task<IActionResult> OnPostSaveAsync()
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

        public async Task<RoleItem> Edit(int id)
        {
            var edit = new RoleItem();

            try
            {
                var existingRole = await _roleServices.GetRoleByIdAsync(id);

                if (existingRole != null)
                {
                    edit.RoleId = existingRole.RoleId;
                    edit.RoleName = existingRole.RoleName;
                }
                else
                {
                    edit.Code = "300";
                    edit.Message = "Role Not found";
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
            var role = await Edit(id);
            return new JsonResult(role);
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var existingRole = await _roleServices.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                FeedBackMessage = "Role not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                FeedBackMessage = "Invalid input";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var dto = new RoleDto
            {
                RoleId = id,
                RoleName = Role.RoleName
            };

            var result = await _roleServices.UpdateRoleAsync(id, dto);
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
        public int? RoleId { get; set; }
        [BindProperty]
        [Required(ErrorMessage ="Role name is Required")]
        [StringLength(100)]
        public string RoleName { get; set; }
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
