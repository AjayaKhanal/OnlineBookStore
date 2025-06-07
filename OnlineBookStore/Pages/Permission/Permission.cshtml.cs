using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.DTO;
using OnlineBookStore.Pages.Permission;
using OnlineBookStore.Services.Implementation;
using OnlineBookStore.Services.Interface;

namespace OnlineBookStore.Pages.Permission
{
    public class PermissionModel : PageModel
    {
        private readonly IPermissionServices _permissionServices;

        public PermissionModel(IPermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
        }

        public List<PermissionItem> Permissions { get; set; } = new List<PermissionItem>();
        [BindProperty]
        public PermissionItem Permission { get; set; } = new PermissionItem();

        [TempData]
        public string? FeedBackMessage { get; set; }

        [TempData]
        public string? FeedbackType { get; set; }

        public async Task OnGet()
        {
            var permissions = await _permissionServices.GetAllPermissionsAsync();
            Permissions = permissions.Select(a => new PermissionItem
            {
                PermissionId = a.PermissionId,
                Name = a.Name,
                Module = a.Module,
                Action = a.Action,
                Resource = a.Resource,
                IsActive = a.IsActive
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

            var dto = new PermissionDto
            {
                Name = Permission.Name,
                Module = Permission.Module,
                Action = Permission.Action,
                Resource = Permission.Resource,
                IsActive = Convert.ToBoolean(Permission.IsActive)
            };

            var result = await _permissionServices.InsertPermissionAsync(dto);

            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var existingPermission = await _permissionServices.GetPermissionByIdAsync(id);
            if (existingPermission == null)
            {
                FeedBackMessage = "Permission not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                FeedBackMessage = "Invalid input";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var dto = new PermissionDto
            {
                PermissionId = id,
                Name = Permission.Name,
                Module = Permission.Module,
                Action = Permission.Action,
                Resource = Permission.Resource,
                IsActive = Permission.IsActive
            };

            var result = await _permissionServices.UpdatePermissionAsync(id, dto);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";
            return RedirectToPage();
        }

        public async Task<PermissionItem> Edit(int id)
        {
            var edit = new PermissionItem();

            try
            {
                var existingPermission = await _permissionServices.GetPermissionByIdAsync(id);

                if (existingPermission != null)
                {
                    edit.PermissionId = existingPermission.PermissionId;
                    edit.Name = existingPermission.Name;
                    edit.Module = existingPermission.Module;
                    edit.Action = existingPermission.Action;
                    edit.Resource = existingPermission.Resource;
                    edit.IsActive = existingPermission.IsActive;
                }
                else
                {
                    edit.Code = "300";
                    edit.Message = "Permission Not found";
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
            var permission = await Edit(id);
            return new JsonResult(permission);
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var existingPermission = await _permissionServices.GetPermissionByIdAsync(id);
            if (existingPermission == null)
            {
                FeedBackMessage = "Permission not found.";
                FeedbackType = "danger";
                return RedirectToPage();
            }

            var result = await _permissionServices.DeletePermissionAsync(id);
            FeedBackMessage = result.Message;
            FeedbackType = result.Code == 200 ? "success" : "danger";
            return RedirectToPage();
        }
    }

    public class PermissionItem
    {
        public int? PermissionId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Module is required.")]
        [StringLength(100)]
        public string Module { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Action is required.")]
        [StringLength(100)]
        public string Action { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Resource is required.")]
        [StringLength(100)]
        public string Resource { get; set; }
        [BindProperty(SupportsGet = false)]
        public bool IsActive { get; set; }
        public string? Message { get; set; }
        public string? Code { get; set; }

    }
}
