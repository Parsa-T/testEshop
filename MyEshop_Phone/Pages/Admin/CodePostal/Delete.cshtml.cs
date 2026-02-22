using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.CodePostal
{
    public class DeleteModel : PageModel
    {
        ICodePostalRepository _codePostalRepository;
        public DeleteModel(ICodePostalRepository codePostal)
        {
            _codePostalRepository = codePostal;
        }
        [BindProperty]
        public _CodePostal DeletePostal { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            if(id==0)
                return NotFound();
            DeletePostal = await _codePostalRepository.FindPostalById(id);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var code = await _codePostalRepository.FindPostalById(DeletePostal.Id);
            if (code == null)
                return BadRequest();
            await _codePostalRepository.DeletePostal(code);
            await _codePostalRepository.Save();
            return RedirectToPage("index");
        }
    }
}
