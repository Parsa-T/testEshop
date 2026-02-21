using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;

namespace MyEshop_Phone.Pages.Admin.AdminOrder
{
    public class IndexModel : PageModel
    {
        IAdminOrdersServices _adminOrdersServices;
        public IndexModel(IAdminOrdersServices admin)
        {
            _adminOrdersServices = admin;
        }
        public IEnumerable<AdminOrderListDTO> AdminOrder { get; set; }
        public async Task OnGet()
        {
            AdminOrder = await _adminOrdersServices.GetFinalOrders();
        }
    }
}
