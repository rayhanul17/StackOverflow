using Autofac;
using Microsoft.AspNetCore.Authorization;
using StackOverflow.Services.DTOs;
using StackOverflow.Services.Services;

namespace StackOverflow.Web.Areas.Admin.Models;

public class QuestionModel : AdminBaseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public DateTime TimeStamp { get; set; }

    private IQuestionService _questionService;
    private ILifetimeScope _scope;

    public QuestionModel()
    {

    }

    public QuestionModel(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _questionService = _scope.Resolve<IQuestionService>();
        base.ResolveDependency(scope);
    }

    public async Task Ask()
    {
        Question question = new Question
        {
            Title = Title,
            VoteCount = VoteCount
        };

        await _questionService.AddAsync(question);
    }

    public async Task Delete(Guid id)
    {
        await _questionService.DeleteAsync(id);    
    }
}
