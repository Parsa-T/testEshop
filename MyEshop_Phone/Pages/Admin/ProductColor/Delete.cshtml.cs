using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System.Drawing;

namespace MyEshop_Phone.Pages.Admin.ProductColor
{
    public class DeleteModel : PageModel
    {
        IPColorServices _services;
        public DeleteModel(IPColorServices pColor)
        {
            _services = pColor;
        }
        [BindProperty]
        public IEnumerable<ProductWithColorsDto> ColorProduct { get; set; }
        public async Task OnGet()
        {
            ColorProduct = await _services.GetProductsWithColors();
        }
    }
}
