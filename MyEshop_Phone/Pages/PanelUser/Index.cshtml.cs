using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.Common;
using MyEshop_Phone.Application.DTO;
using System.Security.Claims;

namespace MyEshop_Phone.Pages.PanelUser
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public UserProfileDTO UserProfile { get; set; }
        public async Task OnGet()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserProfile = await _mediator.Send(new GetUserProfileQuery(userId));
        }

    }
}
