using StackOverflow.Services.BusinessObjects;

namespace StackOverflow.Services;

public interface IQuestionService
{
    Task AddAsync(Question question);
}