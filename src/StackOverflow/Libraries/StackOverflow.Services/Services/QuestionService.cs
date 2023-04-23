using StackOverflow.DAL.UnitOfWorks;
using QuestionBO = StackOverflow.Services.DTOs.Question;
using QuestionEO = StackOverflow.DAL.Entities.Question;

namespace StackOverflow.Services.Services;

public class QuestionService : IQuestionService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ITimeService _timeService;

    public QuestionService(IApplicationUnitOfWork unitOfWork, ITimeService timeService)
    {
        _unitOfWork = unitOfWork;
        _timeService = timeService;
    }

    public async Task AddAsync(QuestionBO question)
    {
        var entity = new QuestionEO();
        entity.Id = question.Id;
        entity.Title = question.Title;
        entity.VoteCount = question.VoteCount;
        entity.TimeStamp = _timeService.Now;
        //entity.Answers = question.Answers;

        _unitOfWork.QuestionRepository.Add(entity);
        _unitOfWork.SaveChanges();
    }
}
