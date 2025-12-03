using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using MyEshop_Phone.Infra.Data.Repository;

namespace MyEshop_Phone.Pages.Admin.Products_Tags
{
    public class DitelseModel : PageModel
    {
        ITagsServices _tagsServices;
        IProductsTagsRepository _productsTagsRepository;
        public DitelseModel(ITagsServices tags,IProductsTagsRepository products)
        {
            _productsTagsRepository = products;
            _tagsServices = tags;
        }
        [BindProperty]
        public IEnumerable<_Products_Tags> DitelseTags { get; set; }
        public async Task OnGet(int id)
        {
            DitelseTags = await _productsTagsRepository.GetAllProductsTagsforId(id);
        }
        public async Task<IActionResult> OnPostDeleteAsync(int tagsId)
        {
            var result = await _productsTagsRepository.DeleteTags(tagsId);

            if (!result)
                return new JsonResult(new { success = false, message = "Record not found" });

            return new JsonResult(new { success = true });
        }
    }
}
