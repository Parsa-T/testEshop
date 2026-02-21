
using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using System.Security.Claims;

public class ShowUserProductViewComponent : ViewComponent
{
    IOrderQuery _orderQuery;
    public ShowUserProductViewComponent(IOrderQuery order)
    {
        _orderQuery = order;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = HttpContext.User as ClaimsPrincipal;

        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return View(new List<UserOrderDto>());

        int userId = int.Parse(userIdClaim.Value);

        var orders = await _orderQuery.GetUserOrders(userId);

        return View(orders);
    }
}
