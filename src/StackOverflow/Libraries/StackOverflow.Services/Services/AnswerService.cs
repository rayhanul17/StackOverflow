using AutoMapper;
using Microsoft.AspNetCore.Http;
using StackOverflow.DAL.UnitOfWorks;
using StackOverflow.Services.Exceptions;
using StackOverflow.Services.Services.Membership;
using System.Security.Cryptography;
using AnswerDto = StackOverflow.Services.DTOs.Answer;
using AnswerEO = StackOverflow.DAL.Entities.Answer;

namespace StackOverflow.Services.Services;

public class AnswerService : IAnswerService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ITimeService _timeService;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public AnswerService(IAccountService accountService,IApplicationUnitOfWork unitOfWork, ITimeService timeService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _timeService = timeService;
        _accountService = accountService;

    }

    public async Task AddAsync(AnswerDto answer)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Description.ToLower() == answer.Description.ToLower()).Count();

        if (count > 0)
            throw new CustomException("You already gave an answer like this");

        var answerEO = _mapper.Map<AnswerEO>(answer);
        answerEO.OwnerId = Guid.Parse(_accountService.GetUserId());
        answerEO.TimeStamp = _timeService.Now;

        var questionEO = _unitOfWork.QuestionRepository.Get(answerEO.QuestionId);
        if(questionEO != null && questionEO.OwnerId == answerEO.OwnerId)
        {
            answerEO.IsApproved = true;
        }

        _unitOfWork.AnswerRepository.Add(answerEO);
        _unitOfWork.SaveChanges();
    }

    public async Task<AnswerDto> GetByIdAsync(Guid id)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Id == id).Count();

        if (count == 0)
            throw new CustomException("Answer Not Found");

        var answerEO = await Task.Run( () => _unitOfWork.AnswerRepository.Get(id));

        return _mapper.Map<AnswerDto>(answerEO);
    }
    public async Task DeleteAsync(Guid answerId)
    {
        _unitOfWork.AnswerRepository.Remove(answerId);
        _unitOfWork.SaveChanges();
    }

    public async Task UpdateAsync(AnswerDto obj)
    {
        var count = _unitOfWork.QuestionRepository.Find(x => x.Id != obj.Id &&
                                                x.Title.ToLower() == obj.Description.ToLower()).Count();

        if (count > 0)
            throw new InvalidOperationException("Question already exists");

        var answerEO = _mapper.Map<AnswerEO>(obj);

        if (answerEO.OwnerId != Guid.Parse(_accountService.GetUserId()))
            throw new CustomException("You are not allowed to customize others questions");
        //questionEO.Category = _mapper.Map<CategoryEO>(_categoryService.GetLazyById(courseEO.CategoryId));

        await Task.Run( () => _unitOfWork.AnswerRepository.Update(answerEO));
        _unitOfWork.SaveChanges();
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Id == id).Count();

        if (count == 0)
            throw new InvalidOperationException("Answer Not Found");

        var answerEO = _unitOfWork.AnswerRepository.Get(id);

        if (answerEO.OwnerId != Guid.Parse(_accountService.GetUserId()))
            throw new CustomException("You are not allowed to delete others answers");

        _unitOfWork.AnswerRepository.Remove(answerEO);
        _unitOfWork.SaveChanges();
    }

    public async Task VoteUpAsync(Guid id)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Id == id).Count();

        if (count == 0)
            throw new InvalidOperationException("Question Not Found");

        var questionEO = _unitOfWork.AnswerRepository.Get(id);

        questionEO.VoteCount++;
        _unitOfWork.SaveChanges();
    }

    public async Task VoteDownAsync(Guid id)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Id == id).Count();

        if (count == 0)
            throw new InvalidOperationException("Question Not Found");

        var questionEO = _unitOfWork.AnswerRepository.Get(id);

        questionEO.VoteCount--;
        _unitOfWork.SaveChanges();
    }

    public async Task ApproveByIdAsync(Guid id)
    {
        var count = _unitOfWork.AnswerRepository.Find(x => x.Id == id).Count();

        if (count == 0)
            throw new InvalidOperationException("Answer Not Found");

        var answerEO = _unitOfWork.AnswerRepository.Get(id);

        answerEO.IsApproved = true;

        _unitOfWork.SaveChanges();
    }

    public async Task<(int total, int totalDisplay, IList<AnswerDto> records)> GetAnswers(int pageIndex,
           int pageSize, string searchText, string orderBy)
    {
        var result = await _unitOfWork.AnswerRepository.GetDynamicAsync(
            x => x.Description.Contains(searchText),
            orderBy,
            pageIndex,
            pageSize);

        var answers = result.data.Select(x => _mapper.Map<AnswerDto>(x)).ToList();
        return (result.total, result.totalDisplay, answers);
    }

    public async Task<(int total, int totalDisplay, IList<AnswerDto> records)> GetPendingAnswers(
        Guid qid, int pageIndex, int pageSize, string searchText, string orderBy)
    {
        var result = await _unitOfWork.AnswerRepository.GetDynamicAsync(
            x => x.Description.Contains(searchText)
                && x.QuestionId.Equals(qid)
                && x.IsApproved.Equals(false),
            orderBy,
            pageIndex, pageSize);

        var answers = result.data.Select(x => _mapper.Map<AnswerDto>(x)).ToList();
        return (result.total, result.totalDisplay, answers);
    }

    public async Task<(int total, int totalDisplay, IList<AnswerDto> records)> GetAnswersByQuestion(Guid qid, int pageIndex,
           int pageSize, string searchText, string orderBy)
    {
        var result = await _unitOfWork.AnswerRepository.GetDynamicAsync(
            x => x.Description.Contains(searchText) 
                && x.QuestionId.Equals(qid)
                && x.IsApproved.Equals(true), 
            orderBy,
            pageIndex,
            pageSize);

        var answers = result.data.Select(x => _mapper.Map<AnswerDto>(x)).ToList();
        return (result.total, result.totalDisplay, answers);
    }
}
