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
                Price = p.Price,
                Count = p.Count,
                ShortDescription = p.ShortDescription,
                ImageName = p.ImageName,
            })
            .ToListAsync(cancellationToken);
    }
}
