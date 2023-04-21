using StackOverflow.DAL.UnitOfWorks;
using QuestionBO = StackOverflow.Services.BusinessObjects.Question;
using QuestionEO = StackOverflow.DAL.Entities.Question;

namespace StackOverflow.Services;

public class QuestionService : IQuestionService
{
    private readonly IApplicationUnitOfWork _unitOfWork;

    public QuestionService(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(QuestionBO question)
    {
        var entity = new QuestionEO();
        entity.Id = question.Id;
        entity.Title = question.Title;
        entity.VoteCount = question.VoteCount;
        //entity.Answers = question.Answers;

        _unitOfWork.QuestionRepository.Add(entity);
        _unitOfWork.SaveChanges();
    }
}
