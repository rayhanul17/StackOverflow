using StackOverflow.Services.DTOs;

namespace StackOverflow.Services.Services;

public interface IAnswerService
{
    Task AddAsync(Answer answer);
    Task DeleteAsync(Guid answerId);
    
}