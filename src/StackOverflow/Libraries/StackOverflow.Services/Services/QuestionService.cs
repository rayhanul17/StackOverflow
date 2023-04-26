using AutoMapper;
using Microsoft.AspNetCore.Http;
using StackOverflow.DAL.UnitOfWorks;
using StackOverflow.Services.Exceptions;
using StackOverflow.Services.Services.Membership;
using QuestionDto = StackOverflow.Services.DTOs.Question;
using QuestionEO = StackOverflow.DAL.Entities.Question;

namespace StackOverflow.Services.Services;

public class QuestionService : IQuestionService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ITimeService _timeService;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;
    private readonly Guid _userId;

    public QuestionService(IAccountService accountService,IApplicationUnitOfWork unitOfWork, ITimeService timeService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _timeService = timeService;
        _accountService = accountService;
        _userId = Guid.Parse(_accountService.GetUserId());
    }

    public async Task AddAsync(QuestionDto question)
    {
        var count = _unitOfWork.QuestionRepository.Find(x => x.Title.ToLower() == question.Title.ToLower()).Count();

        if (count > 0)
            throw new CustomException("Question already exists");

        //var entity = new QuestionEO();
        //entity.Id = question.Id;
        //entity.Title = question.Title;
        //entity.VoteCount = question.VoteCount;
        //entity.TimeStamp = _timeService.Now;
        //entity.OwnerId = Guid.Parse(_accountService.GetUserId());
        ////entity.Answers = question.Answers;

        var questionEO = _mapper.Map<QuestionEO>(question);

        _unitOfWork.QuestionRepository.Add(questionEO);
        _unitOfWork.SaveChanges();
    }

    public async Task<QuestionDto> GetByIdAsync(Guid id)
    {
        var entity = await Task.Run(() => _unitOfWork.QuestionRepository.Get(id));

        if (entity is null)
            throw new CustomException("Question Not Found");

        if (entity.OwnerId != _userId)
            throw new CustomException("You are not allowed");

        //var questionEO = await Task.Run( () => _unitOfWork.QuestionRepository.Get(id));

        return _mapper.Map<QuestionDto>(entity);
    }
    public async Task DeleteAsync(Guid questionId)
    {
        _unitOfWork.QuestionRepository.Remove(questionId);
        _unitOfWork.SaveChanges();
    }

    public async Task UpdateAsync(QuestionDto obj)
    {
        var count = _unitOfWork.QuestionRepository.Find(x => x.Id != obj.Id &&
                                                x.Title.ToLower() == obj.Title.ToLower()).Count();

        if (count > 0)
            throw new InvalidOperationException("Question already exists");

        var questionEO = _mapper.Map<QuestionEO>(obj);
        //questionEO.Category = _mapper.Map<CategoryEO>(_categoryService.GetLazyById(courseEO.CategoryId));

        await Task.Run( () => _unitOfWork.QuestionRepository.Update(questionEO));
        _unitOfWork.SaveChanges();
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var count = _unitOfWork.QuestionRepository.Find(x => x.Id == id).Count();

        if (count == 0)
            throw new InvalidOperationException("Question Not Found");

        var qustionEO = _unitOfWork.QuestionRepository.Get(id);

        _unitOfWork.QuestionRepository.Remove(qustionEO);
        _unitOfWork.SaveChanges();
    }

    public async Task<(int total, int totalDisplay, IList<QuestionDto> records)> GetQuestions(int pageIndex,
           int pageSize, string searchText, string orderBy)
    {
        var result = await _unitOfWork.QuestionRepository.GetDynamicAsync(x => x.Title.Contains(searchText), orderBy,
            pageIndex, pageSize);

        var questions = result.data.Select(x => _mapper.Map<QuestionDto>(x)).ToList();
        return (result.total, result.totalDisplay, questions);
    }

}
