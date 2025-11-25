using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        IUserServices _userServices;
        public IndexModel(IUserServices user)
        {
            _userServices = user;
        }
        public IEnumerable<_Users> GetAllUsers { get; set; }
        public async Task OnGet()
        {
            GetAllUsers = await _userServices.GetUsers();
        }
    }
}
