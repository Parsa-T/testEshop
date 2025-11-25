using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Users
{
    public class AddModel : PageModel
    {
        IUserServices _userServices;
        public AddModel(IUserServices user)
        {
            _userServices = user;
        }
        [BindProperty]
        public AddOrEditUsersDTO UserAdd { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            _Users user = new _Users()
            {
                Address = UserAdd.Address,
                Family = UserAdd.Family,
                Name = UserAdd.Name,
                Number = UserAdd.Number,
                RegisterDate = DateTime.Now,
                IsAdmin = UserAdd.IsAdmin,
                UrlPhoto = UserAdd.Name,
            };
            if(!ModelState.IsValid)
                return Page();

            await _userServices.AddUsers(user);
            await _userServices.SaveAsync();
            if (UserAdd.imgUp?.Length > 0)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", user.Id
                    + Path.GetExtension(UserAdd.imgUp.FileName));
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    UserAdd.imgUp.CopyTo(stream);
                }
                user.UrlPhoto = user.Id + Path.GetExtension(UserAdd.imgUp.FileName);
                await _userServices.SaveAsync();
            }
            return RedirectToPage("index");
        }
    }
}
