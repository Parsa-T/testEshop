using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Pages.Admin.SubMenuGroups
{
    public class EditModel : PageModel
    {
        ISubGroupsServices _subGroupsServices;
        public EditModel(ISubGroupsServices groupsServices)
        {
            _subGroupsServices = groupsServices;
        }
        [BindProperty]
        public SubMenuGroupsDTO EditSubMenu { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            EditSubMenu = await _subGroupsServices.GetSubGroupsForId(id);
            if (EditSubMenu == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            await _subGroupsServices.UpdateSubMenuAsync(EditSubMenu);
            return RedirectToPage("index");
        }
    }
}
