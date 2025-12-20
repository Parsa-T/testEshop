using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.ProductColor
{
    public class Delete02Model : PageModel
    {
        IPColorServices _colorServices;
        public Delete02Model(IPColorServices pColor)
        {
            _colorServices = pColor;
        }
        [BindProperty]
        public IEnumerable<_ProductsColor> DeleteColor { get; set; }
        public async Task OnGet()
        {
            DeleteColor = await _colorServices.showAllColor();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int colorId)
        {
            var result = await _colorServices.Delete(colorId);
            if (!result)
                return new JsonResult(new { success = false, message = "Record not found" });
            return new JsonResult(new { success = true });
        }
    }
}
