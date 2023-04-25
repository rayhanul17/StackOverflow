using AutoMapper;
using ApplicationUserEO = StackOverflow.DAL.Entities.Membership.ApplicationUser;
using ApplicationUserDto = StackOverflow.Services.DTOs.Membership.ApplicationUser;
using QuestionEO = StackOverflow.DAL.Entities.Question;
using QuestionDto = StackOverflow.Services.DTOs.Question;

namespace StackOverflow.Services.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ApplicationUserEO, ApplicationUserDto>().ReverseMap();
        CreateMap<QuestionEO, QuestionDto>().ReverseMap();
    }
}
