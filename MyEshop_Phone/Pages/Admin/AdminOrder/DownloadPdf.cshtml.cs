using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Utilitise;
using QuestPDF.Fluent;
using System.Reflection.Metadata;

namespace MyEshop_Phone.Pages.Admin.AdminOrder
{
    public class DownloadPdfModel : PageModel
    {
        IAdminOrdersServices _adminOrders;
        public DownloadPdfModel(IAdminOrdersServices admin)
        {
            _adminOrders = admin;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var order = await _adminOrders.GetOrderForPdf(id);
            if(order==null)
                return NotFound();
            if (order == null)
                return NotFound();

            if (order.Items == null)
                throw new Exception("Items is null");
            var document = new OrderPdfDocument(order);
            var pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Order-{id}.pdf");
        }
    }
}
