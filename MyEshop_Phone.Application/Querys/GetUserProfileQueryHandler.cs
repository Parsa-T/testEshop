using MediatR;
using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.Common.Interfaces;
using MyEshop_Phone.Application.DTO;

public class GetUserProfileQueryHandler
    : IRequestHandler<GetUserProfileQuery, UserProfileDTO>
{
    private readonly IApplicationDbContext _context;

    public GetUserProfileQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileDTO> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => u.Id == int.Parse(request.UserId))
            .Select(u => new UserProfileDTO
            {
                Address = u.Address,
                CityId = u.CityId,
                CityName = u.CityName,
                Family = u.Family,
                Name = u.Name,
                Number = u.Number,
                PostalCode = u.PostalCode,
                StateId = u.StateId,
                StateName = u.StateName,
                UrlPhoto = u.UrlPhoto,
                Id = u.Id
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
