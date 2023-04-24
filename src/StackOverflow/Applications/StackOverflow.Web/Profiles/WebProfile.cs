using AutoMapper;
using StackOverflow.Services.DTOs.Membership;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Profiles;

public class WebProfile : Profile
{
    public WebProfile()
    {
        CreateMap<ApplicationUser, RegisterModel>().ReverseMap()
            .ConvertUsing(x => new ApplicationUser
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.Email,
                Email = x.Email,
                Password = x.Password
            });

        CreateMap<ApplicationUser, LoginModel>().ReverseMap();       
    }
}
