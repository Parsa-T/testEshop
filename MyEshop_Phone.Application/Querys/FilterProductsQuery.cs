using MediatR;
using MyEshop_Phone.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FilterProductsQuery : IRequest<List<ProductsDTO>>
{
    public string? Q { get; set; }
    public int? GroupId { get; set; }
    public int? SubMenuId { get; set; }
    public string? Category { get; set; }
    public List<string>? Brands { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool InStock { get; set; }
}
