using AutoMapper;
using StackOverflow.DAL.UnitOfWorks;
using AnswerDto = StackOverflow.Services.DTOs.Answer;
using AnswerEO = StackOverflow.DAL.Entities.Answer;

namespace StackOverflow.Services.Services;

public class AnswerService : IAnswerService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ITimeService _timeService;
    private readonly IMapper _mapper;

    public AnswerService(IApplicationUnitOfWork unitOfWork, ITimeService timeService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _timeService = timeService;
    }

    public async Task AddAsync(AnswerDto answer)
    {
        var entity = new AnswerEO();
        entity.Id = answer.Id;
        entity.Description = answer.Description;
        entity.VoteCount = answer.VoteCount;
        entity.TimeStamp = _timeService.Now;
        //entity.Answers = question.Answers;

        _unitOfWork.AnswerRepository.Add(entity);
        _unitOfWork.SaveChanges();
    }

    public async Task DeleteAsync(Guid answerId)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Id == answerId).Count();
        var entity = _unitOfWork.AnswerRepository.Get(answerId);

        _unitOfWork.AnswerRepository.Remove(entity);
        _unitOfWork.SaveChanges();
    }    
}
