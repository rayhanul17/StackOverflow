using Autofac;
using StackOverflow.Services.DTOs;
using StackOverflow.Services.Services;

namespace StackOverflow.Web.Areas.Admin.Models;

public class AnswerModel : AdminBaseModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }
    public int VoteCount { get; set; }

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

    public void Ask()
    {
        var question = new Answer()
        {
            Description = Description
        };

        _answerService.AddAsync(question);
    }
}
