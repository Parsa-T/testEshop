using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Features
{
    public class IndexModel : PageModel
    {
        IFeaturseRepository _featurseRepository;
        public IndexModel(IFeaturseRepository featurse)
        {
            _featurseRepository = featurse;
        }
        public IEnumerable<_Features> AllFeatures { get; set; }
        public async Task OnGet()
        {
            AllFeatures = await _featurseRepository.GetAll();
        }
    }
}
