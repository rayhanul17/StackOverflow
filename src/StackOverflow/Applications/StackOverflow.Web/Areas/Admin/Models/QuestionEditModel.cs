using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverflow.Services.DTOs;
using StackOverflow.Services.Services;

namespace StackOverflow.Web.Areas.Admin.Models;

public class QuestionEditModel : AdminBaseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime TimeStamp { get; set; }
    public List<AnswerModel>? Answers { get; set; }

    private IQuestionService _questionService;
    private ILifetimeScope _scope;
    private IMapper _mapper;

    public QuestionEditModel()
    {

    }

    public QuestionEditModel(IQuestionService questionService, IMapper mapper)
    {
        _questionService = questionService;
        _mapper = mapper;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _questionService = _scope.Resolve<IQuestionService>();
        _mapper = _scope.Resolve<IMapper>();
        base.ResolveDependency(scope);
    }

    public async void GetQuestion(Guid id)
    {
        var question = await _questionService.GetByIdAsync(id);

        if (question != null)
        {
            _mapper.Map(question, this);
        }
    }

    public async Task UpdateCourseAsync()
    { 
        var course = _mapper.Map<Question>(this);
        await _questionService.UpdateAsync(course);
    }
}
