using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyEshop_Phone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilterProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] FilterProductsQuery filter)
        {
            var result = await _mediator.Send(filter);
            return Ok(result);
        }

    }
}
