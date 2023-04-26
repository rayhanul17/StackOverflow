using AutoMapper;
using StackOverflow.Services.DTOs;
using StackOverflow.Services.DTOs.Membership;
using StackOverflow.Web.Areas.Admin.Models;
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
        CreateMap<Question, GetQuestionsModel>().ReverseMap();       
        CreateMap<Question, QuestionEditModel>().ReverseMap();       
        CreateMap<Answer, AnswerModel>().ReverseMap();       
    }
}
