using AutoMapper;
using ApplicationUserEO = StackOverflow.DAL.Entities.Membership.ApplicationUser;
using ApplicationUserDto = StackOverflow.Services.DTOs.Membership.ApplicationUser;

namespace StackOverflow.Services.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ApplicationUserEO, ApplicationUserDto>().ReverseMap();
    }
}
