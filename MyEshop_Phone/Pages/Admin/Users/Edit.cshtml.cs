using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;

namespace MyEshop_Phone.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        IUserServices _userServices;
        IUserRepository _userRepository;
        private readonly IWebHostEnvironment _env;
        public EditModel(IUserServices user, IUserRepository repository, IWebHostEnvironment env)
        {
            _userServices = user;
            _userRepository = repository;
            _env = env;
        }
        [BindProperty]
        public AddOrEditUsersDTO EditUser { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            EditUser = await _userServices.GetUserforEdit(id);
            if (EditUser == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var users = await _userRepository.GetUserById(EditUser.Id);
            if (users == null)
                return NotFound();
            users.Number = EditUser.Number;
            users.Address = EditUser.Address;
            users.Id = EditUser.Id;
            users.Name = EditUser.Name;
            users.IsAdmin = EditUser.IsAdmin;
            users.Family = EditUser.Family;
            users.PostalCode = EditUser.PostalCode;
            await _userServices.SaveAsync();
            if (EditUser.imgUp?.Length > 0 && EditUser.imgUp != null)
            {
                string uploadsFolder = Path.Combine(
    _env.WebRootPath,
    "AdminPanel",
    "Photo",
    "Users"
);

                Directory.CreateDirectory(uploadsFolder);
                if (!string.IsNullOrEmpty(users.UrlPhoto))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, users.UrlPhoto);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                string extension = Path.GetExtension(EditUser.imgUp.FileName);
                string newFileName = $"{users.Id}{extension}";
                string newFilePath = Path.Combine(uploadsFolder, newFileName);
                using (var stream = new FileStream(newFilePath,FileMode.Create))
                {
                    await EditUser.imgUp.CopyToAsync(stream);
                }
                users.UrlPhoto = newFileName;
                await _userServices.SaveAsync();
                //string filepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                //    + ".jpg");
                //if(System.IO.File.Exists(filepath2))
                //    System.IO.File.Delete(filepath2);
                //string filepath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                //    + ".png");
                //if (System.IO.File.Exists(filepath3))
                //    System.IO.File.Delete(filepath3);
                //string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", users.Id
                //    + Path.GetExtension(EditUser.imgUp.FileName));
                //using (var stream = new FileStream(filepath, FileMode.Create))
                //{
                //    EditUser.imgUp.CopyTo(stream);
                //}
                //users.UrlPhoto = users.Id + Path.GetExtension(EditUser.imgUp.FileName);
                //await _userServices.SaveAsync();
            }
            return RedirectToPage("index");
        }
    }
}
