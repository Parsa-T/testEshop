using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Features
{
    public class DeleteModel : PageModel
    {
        IFeaturseRepository _featurseRepository;
        public DeleteModel(IFeaturseRepository repository)
        {
            _featurseRepository = repository;
        }
        [BindProperty]
        public _Features DeleteFeatures { get; set; }
        public async Task OnGet(int id)
        {
            DeleteFeatures = await _featurseRepository.GetByIdAsync(id);
        }
        public async Task<IActionResult> OnPost()
        {
            var f = await _featurseRepository.GetByIdAsync(DeleteFeatures.Id);
            await _featurseRepository.DeleteFeatures(f);
            await _featurseRepository.Save();
            return RedirectToPage("Index");
        }
    }
}
