using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverflow.Services.DTOs;
using StackOverflow.Services.Services;

namespace StackOverflow.Web.Areas.Admin.Models;

public class AnswerEditModel : AdminBaseModel
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public Guid QuestionId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public DateTime TimeStamp { get; set; }
    public bool IsApproved { get; set; }


    private IAnswerService _answerService;
    private ILifetimeScope _scope;
    private IMapper _mapper;

    public AnswerEditModel()
    {

    }

    public AnswerEditModel(IAnswerService answerService, IMapper mapper)
    {
        _answerService = answerService;
        _mapper = mapper;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _answerService = _scope.Resolve<IAnswerService>();
        _mapper = _scope.Resolve<IMapper>();
        base.ResolveDependency(scope);
    }

    public async void GetAnswer(Guid id)
    {
        var answer = await _answerService.GetByIdAsync(id);

        if (answer != null)
        {
            _mapper.Map(answer, this);
        }
    }

    public async Task UpdateAnswerAsync()
    { 
        var answer = _mapper.Map<Answer>(this);
        await _answerService.UpdateAsync(answer);
    }
}
