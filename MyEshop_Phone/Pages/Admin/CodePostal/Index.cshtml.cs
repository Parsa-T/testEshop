using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.CodePostal
{
    public class IndexModel : PageModel
    {
        ICodePostalRepository _codePostalRepository;
        public IndexModel(ICodePostalRepository codePostal)
        {
            _codePostalRepository = codePostal;
        }
        public IEnumerable<_CodePostal> AllPostal { get; set; }
        public async Task OnGet()
        {
            AllPostal = await _codePostalRepository.GetAllPostal();
        }
    }
}
