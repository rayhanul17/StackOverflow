using StackOverflow.Services.DTOs;

namespace StackOverflow.Services.Services;

public interface IQuestionService
{
    Task AddAsync(Question question);
}