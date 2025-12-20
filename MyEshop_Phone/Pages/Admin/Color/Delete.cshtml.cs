using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Color
{
    public class DeleteModel : PageModel
    {
        IColorRepository _colorRepository;
        public DeleteModel(IColorRepository repository)
        {
            _colorRepository = repository;
        }
        [BindProperty]
        public IEnumerable<_Color> DeleteColor { get; set; }
        public async Task OnGet()
        {
            DeleteColor = await _colorRepository.GetAllAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int colorId)
        {
            var result = await _colorRepository.DeleteAsync(colorId);
            if(!result)
                return new JsonResult(new { success = false, message = "Record not found" });
            return new JsonResult(new { success = true });
        }
    }
}
