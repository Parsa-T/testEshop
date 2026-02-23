using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Utilitise;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Users
{
    public class AddModel : PageModel
    {
        IUserServices _userServices;
        ILocationServices _locationServices;
        private readonly IWebHostEnvironment _env;
        public AddModel(IUserServices user, ILocationServices location, IWebHostEnvironment env)
        {
            _userServices = user;
            _locationServices = location;
            _env = env;
        }
        public List<StateDto> States { get; set; } = new();
        [BindProperty]
        public AddOrEditUsersDTO UserAdd { get; set; }
        public async Task OnGet()
        {
            States = await _locationServices.GetStatesAsync();
        }
        public async Task<JsonResult> OnGetCitiesAsync(int stateId)
        {
            var cities = await _locationServices.GetCitiesAsync(stateId);
            return new JsonResult(cities);
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("UserAdd.ProductsId");
            ModelState.Remove("UserAdd.StateName");
            ModelState.Remove("UserAdd.CityName");
            ModelState.Remove("UserAdd.CodeActive");
            if (!ModelState.IsValid)
                return Page();
            var states = await _locationServices.GetStatesAsync();
            var state = states.First(x => x.Id == UserAdd.StateId);

            var cities = await _locationServices.GetCitiesAsync(UserAdd.StateId);
            var city = cities.First(x => x.Id == UserAdd.CityId);
            _Users user = new _Users()
            {
                Address = UserAdd.Address,
                Family = UserAdd.Family,
                Name = UserAdd.Name,
                Number = UserAdd.Number,
                RegisterDate = DateTime.Now,
                IsAdmin = UserAdd.IsAdmin,
                UrlPhoto = null,
                CityName = city.Name,
                StateName = state.Name,
                PostalCode = UserAdd.PostalCode,
                CityId = city.Id,
                StateId = state.Id,
            };

            await _userServices.AddUsers(user);
            await _userServices.SaveAsync();
            if (UserAdd.imgUp?.Length > 0 && UserAdd.imgUp != null)
            {
                //string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Users", user.Id
                //    + Path.GetExtension(UserAdd.imgUp.FileName));
                string uploadsFolder = Path.Combine(
    _env.WebRootPath,
    "AdminPanel",
    "Photo",
    "Users"
);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string fileName = user.Id + Path.GetExtension(UserAdd.imgUp.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
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
