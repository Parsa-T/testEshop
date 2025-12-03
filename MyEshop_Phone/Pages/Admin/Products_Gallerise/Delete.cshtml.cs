using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.Products_Gallerise
{
    public class DeleteModel : PageModel
    {
        IGalleriseRepository _galleriseRepository;
        public DeleteModel(IGalleriseRepository gallerise)
        {
            _galleriseRepository = gallerise;
        }
        [BindProperty]
        public _Products_Galleries DeleteGalleris { get; set; }
        public async Task OnGet(int id)
        {
            DeleteGalleris = await _galleriseRepository.ShowGalleris(id);
        }
        public async Task<IActionResult> OnPost()
        {
            var gp = await _galleriseRepository.GetByIdAsync(DeleteGalleris.Id);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminPanel", "Photo", "Products", "Gallerise", gp.ImageName);
            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            await _galleriseRepository.DeleteAsync(gp);
            await _galleriseRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
