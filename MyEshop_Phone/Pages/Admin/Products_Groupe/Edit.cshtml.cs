using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;

namespace MyEshop_Phone.Pages.Admin.Products_Groupe
{
    public class EditModel : PageModel
    {
        IProductsGroupServices _productsGroupServices;
        IProductsGroupeRepository _productsGroupeRepository;
        public EditModel(IProductsGroupServices services, IProductsGroupeRepository productsGroupeRepository)
        {
            _productsGroupServices = services;
            _productsGroupeRepository = productsGroupeRepository;
        }
        [BindProperty]
        public AddOrEditGroupsDTO EditGroups { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            EditGroups = await _productsGroupServices.GetGroupsForEdit(id);
            if (EditGroups == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var groups = await _productsGroupeRepository.GetGroupsById(EditGroups.Id);
            if(groups==null)
                return NotFound();

            groups.GroupTitle = EditGroups.GroupTitle;
            await _productsGroupServices.Save();
            return RedirectToPage("index");
        }
    }
}
