using Autofac;
using Microsoft.AspNetCore.Authorization;
using StackOverflow.Services.DTOs;
using StackOverflow.Services.Services;

namespace StackOverflow.Web.Areas.Admin.Models;

public class AnswerModel : AdminBaseModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;   
    public Guid QuestionId { get; set; }

    private IAnswerService _answerService;
    private ILifetimeScope _scope;

    public AnswerModel()
    {

    }

    public AnswerModel(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _answerService = _scope.Resolve<IAnswerService>();
        base.ResolveDependency(scope);
    }

    public async Task Add()
    {
        var answer = new Answer { Description = Description, QuestionId = QuestionId };

        await _answerService.AddAsync(answer);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _answerService.RemoveByIdAsync(id);    
    }

    public async Task ApproveAsync(Guid id)
    {
        await _answerService.ApproveByIdAsync(id);
    }

    public async Task VoteUpAsync(Guid id)
    {
        await _answerService.VoteUpAsync(id);
    }

    public async Task VoteDownAsync(Guid id)
    {
        await _answerService.VoteDownAsync(id);
    }
}
