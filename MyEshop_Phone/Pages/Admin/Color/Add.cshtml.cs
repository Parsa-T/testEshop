using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Color
{
    public class AddModel : PageModel
    {
        IColorRepository _colorRepository;
        public AddModel(IColorRepository color)
        {
            _colorRepository = color;
        }
        [BindProperty]
        public _Color AddColor { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("AddColor.productsColors");
            if (!ModelState.IsValid)
                return Page();
            var color = new _Color()
            {
                Name = AddColor.Name,
            };
            await _colorRepository.AddAsync(color);
            return RedirectToPage("Delete");
        }
    }
}
