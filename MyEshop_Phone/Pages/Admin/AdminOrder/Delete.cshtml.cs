using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;

namespace MyEshop_Phone.Pages.Admin.AdminOrder
{
    public class DeleteModel : PageModel
    {
        IOrderRepository _orderRepository;
        public DeleteModel(IOrderRepository order)
        {
            _orderRepository = order;
        }
        [BindProperty]
        public _Orders DeleteOrder { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            if (id == null)
                return NotFound();
            DeleteOrder = await _orderRepository.FindById(id);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var order = await _orderRepository.FindById(DeleteOrder.Id);
            if(order==null)
                return NotFound();
            await _orderRepository.DeleteOrder(order);
            return RedirectToPage("index");
        }
    }
}
