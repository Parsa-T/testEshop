using MediatR;
using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.Common.Interfaces;
using MyEshop_Phone.Application.DTO;

public class FilterProductsQueryHandler : IRequestHandler<FilterProductsQuery, List<ProductsDTO>>
{
    private readonly IApplicationDbContext _context;

    public FilterProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<ProductsDTO>> Handle(
       FilterProductsQuery request,
       CancellationToken cancellationToken)
    {
        var query = _context.Products.Include(p=>p.products_Groups).Include(s=>s.submenuGroups).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            query = query.Where(p =>
                (p.Title ?? string.Empty).Contains(q) ||
                (p.ShortDescription ?? string.Empty).Contains(q) ||
                (p.Text ?? string.Empty).Contains(q));
        }

        if (request.GroupId.HasValue)
            query = query.Where(p => p.ProductGroupsId == request.GroupId.Value);

        if (request.SubMenuId.HasValue)
            query = query.Where(p => p.SubmenuGroupsId == request.SubMenuId.Value);

        if (!string.IsNullOrEmpty(request.Category) && request.Category != "all")
            query = query.Where(p => p.products_Groups.GroupTitle == request.Category);

        if (request.Brands != null && request.Brands.Any())
            query = query.Where(p => request.Brands.Contains(p.ShortDescription));

        if (request.MinPrice.HasValue)
            query = query.Where(p => p.Price >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= request.MaxPrice.Value);

        if (request.InStock)
            query = query.Where(p => p.Count > 0);

        return await query
            .Select(p => new ProductsDTO
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.products_Groups != null ? p.products_Groups.GroupTitle : string.Empty,
                Price = p.Price,
                Count = p.Count,
                ShortDescription = p.ShortDescription,
                ImageName = p.ImageName,
            })
            .ToListAsync(cancellationToken);
    }
}
