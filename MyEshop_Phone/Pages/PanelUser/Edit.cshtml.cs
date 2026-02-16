using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;

namespace MyEshop_Phone.Pages.PanelUser
{
    public class EditModel : PageModel
    {
        IUserServices _userServices;
        IUserRepository _userRepository;
        public EditModel(IUserServices user, IUserRepository repository)
        {
            _userServices = user;
            _userRepository = repository;
        }
        [BindProperty]
        public EditUserProfileDTO EditUser { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            EditUser = await _userServices.UserforEdit(id);
            if (EditUser == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var users = await _userRepository.GetUserById(EditUser.Id);
            if (users == null)
                return NotFound();
            users.Family = EditUser.Family;
            users.PostalCode = EditUser.PostalCode;
            users.Address = EditUser.Address;
            users.Id = EditUser.Id;
            users.Name = EditUser.Name;
            users.CityName = EditUser.CityName;
            users.StateName = EditUser.StateName;
            await _userServices.SaveAsync();
            if (EditUser.imgUp?.Length > 0)
            {
                string filepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                    + ".jpg");
                if (System.IO.File.Exists(filepath2))
                    System.IO.File.Delete(filepath2);
                string filepath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                    + ".png");
                if (System.IO.File.Exists(filepath3))
                    System.IO.File.Delete(filepath3);
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                    + Path.GetExtension(EditUser.imgUp.FileName));
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    EditUser.imgUp.CopyTo(stream);
                }
                users.UrlPhoto = users.Id + Path.GetExtension(EditUser.imgUp.FileName);
                await _userServices.SaveAsync();
            }
            return RedirectToPage("index");
        }
    }
}
