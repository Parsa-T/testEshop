using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.SubMenuGroups
{
    public class AddModel : PageModel
    {
        ISubGroupsServices _subGroupsServices;
        public AddModel(ISubGroupsServices subGroups)
        {
            _subGroupsServices = subGroups;
        }
        [BindProperty]
        public SubMenuGroupsDTO AddSubMenu { get; set; }
        public async Task<IActionResult> OnGet()
        {
            AddSubMenu = await _subGroupsServices.ShowAllSubGroups();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("AddSubMenu.ShowAllGroups");
            if(!ModelState.IsValid)
                return Page();
            var subMenu = new _SubmenuGroups
            {
                Title = AddSubMenu.Title,
                Products_GroupsId = AddSubMenu.Products_GroupsId,
            };
            await _subGroupsServices.AddSubMenuAsync(subMenu);
            return RedirectToPage("index");
        }
    }
}
