using MediatR;
using MyEshop_Phone.Application.DTO;

public record GetUserProfileQuery(string UserId)
    : IRequest<UserProfileDTO>;

