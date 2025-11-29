using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products_Groupe
{
    public class DeleteModel : PageModel
    {
        IProductsGroupServices _productsGroupServices;
        IProductsGroupeRepository _productsGroupeRepository;
        public DeleteModel(IProductsGroupServices productsGroupServices,IProductsGroupeRepository products)
        {
            _productsGroupeRepository = products;
            _productsGroupServices = productsGroupServices;
        }
        [BindProperty]
        public _Products_Groups DeleteGroups { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            DeleteGroups = await _productsGroupeRepository.GetGroupsById(id);
            if (DeleteGroups == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var group = await _productsGroupeRepository.GetGroupsById(DeleteGroups.Id);
            if (group == null)
                return BadRequest();
            _productsGroupServices.RemoveGroups(group);
            await _productsGroupServices.Save();
            return RedirectToPage("index");
        }
    }
}
