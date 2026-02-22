using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.CodePostal
{
    public class AddModel : PageModel
    {
        IPostalCodeServices _postalCodeServices;
        ICodePostalRepository _codePostalRepository;
        public AddModel(IPostalCodeServices postalCode,ICodePostalRepository postalRepository)
        {
            _postalCodeServices = postalCode;
            _codePostalRepository = postalRepository;
        }
        [BindProperty]
        public ShowUserAndProductsDTO AddPostalCode { get; set; }
        public async Task OnGet()
        {
            AddPostalCode = await _postalCodeServices.GetAllAsync();
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("AddPostalCode.ShowUsers");
            ModelState.Remove("AddPostalCode.ShowProducts");
            if (ModelState.IsValid)
            {
                var code = new _CodePostal()
                {
                    Code = AddPostalCode.Code,
                    ProductsId = AddPostalCode.ProductsId,
                    UserId = AddPostalCode.UserId,

                };
                await _codePostalRepository.Add(code);
                await _codePostalRepository.Save();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
