using Autofac;
using StackOverflow.Services;
using StackOverflow.Services.BusinessObjects;

namespace StackOverflow.Web.Areas.Admin.Models;

public class QuestionModel : AdminBaseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public List<AnswerModel>? Answers { get; set; }

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

    public void Ask()
    {
        Question question = new Question
        {
            Title = Title,
            VoteCount = VoteCount
        };

        _questionService.AddAsync(question);
    }
}
