using AutoMapper;
using StackOverflow.DAL.UnitOfWorks;
using QuestionDto = StackOverflow.Services.DTOs.Question;
using QuestionEO = StackOverflow.DAL.Entities.Question;

namespace StackOverflow.Services.Services;

public class QuestionService : IQuestionService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ITimeService _timeService;
    private readonly IMapper _mapper;

    public QuestionService(IApplicationUnitOfWork unitOfWork, ITimeService timeService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _timeService = timeService;
    }

    public async Task AddAsync(QuestionDto question)
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

    public async Task DeleteAsync(Guid questionId)
    {
        var count = _unitOfWork.QuestionRepository.Find(x => x.Id == questionId).Count();
        var entity = _unitOfWork.QuestionRepository.Get(questionId);

        _unitOfWork.QuestionRepository.Remove(entity);
        _unitOfWork.SaveChanges();
    }

    public async Task<(int total, int totalDisplay, IList<QuestionDto> records)> GetQuestionsAsync(int pageIndex,
           int pageSize, string searchText, string orderBy)
    {
        var result = await _unitOfWork.QuestionRepository.GetDynamicAsync(x => x.Title.Contains(searchText), orderBy,
            pageIndex, pageSize);

        var questions = result.data.Select(x => _mapper.Map<QuestionDto>(x)).ToList();
        return (result.total, result.totalDisplay, questions);
    }
}
