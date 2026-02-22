
using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Interface;
using System.Security.Claims;

public class SendProductUserViewComponent : ViewComponent
{
    ICodePostalRepository _codePostalRepository;
    public SendProductUserViewComponent(ICodePostalRepository code)
    {
        _codePostalRepository = code;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = HttpContext.User as ClaimsPrincipal;

        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return View(new List<UserOrderDto>());
        var userId = int.Parse(userIdClaim.Value);
        var model = await _codePostalRepository.ShowSenderForId(userId);
        return View(model);
    }
}
