using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.SubMenuGroups
{
    public class IndexModel : PageModel
    {
        ISubGroupsServices _subGroupsServices;
        public IndexModel(ISubGroupsServices subGroups)
        {
            _subGroupsServices = subGroups;
        }
        public IEnumerable<_SubmenuGroups> submenuGroups { get; set; }
        public async Task OnGet()
        {
            submenuGroups = await _subGroupsServices.ShowSubMenuGroups();
        }
    }
}
