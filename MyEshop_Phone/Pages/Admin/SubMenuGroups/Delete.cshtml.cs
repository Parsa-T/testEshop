using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.SubMenuGroups
{
    public class DeleteModel : PageModel
    {
        ISubMenuGroupsRepository _subMenuGroupsRepository;
        public DeleteModel(ISubMenuGroupsRepository subMenuGroups)
        {
            _subMenuGroupsRepository = subMenuGroups;
        }
        [BindProperty]
        public _SubmenuGroups DeleteSubMenu { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            DeleteSubMenu = await _subMenuGroupsRepository.ShowSubMenuForId(id);
            if(DeleteSubMenu==null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            await _subMenuGroupsRepository.DeleteSubMenu(DeleteSubMenu);
            return RedirectToPage("index");
        }
    }
}
