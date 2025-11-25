using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        IUserRepository _userRepository;
        IUserServices _userServices;
        public DeleteModel(IUserRepository user,IUserServices services)
        {
            _userRepository = user;
            _userServices = services;
        }
        [BindProperty]
        public _Users DeleteUsers { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            DeleteUsers = await _userRepository.GetUserById(id);

            if (DeleteUsers == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var users = await _userRepository.GetUserById(DeleteUsers.Id);
            if (users == null)
                return BadRequest();

            string filepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                    + ".jpg");
            if (System.IO.File.Exists(filepath2))
                System.IO.File.Delete(filepath2);

            string filepath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                + ".png");
            if (System.IO.File.Exists(filepath3))
                System.IO.File.Delete(filepath3);

            _userServices.UserDelete(users);
            await _userServices.SaveAsync();
            return RedirectToPage("index");
        }
    }
}
