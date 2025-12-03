using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Features
{
    public class AddModel : PageModel
    {
        IFeaturseRepository _featurseRepository;
        public AddModel(IFeaturseRepository repository)
        {
            _featurseRepository = repository;
        }
        [BindProperty]
        public _Features AddFeatures { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        {
            ModelState.Remove("AddFeatures.products_Features");
            if(!ModelState.IsValid)
                return Page();
            await _featurseRepository.AddFeaturse(AddFeatures);
            await _featurseRepository.Save();
            return RedirectToPage("index");
        }
    }
}
