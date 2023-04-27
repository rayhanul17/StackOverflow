using StackOverflow.Services.DTOs;

namespace StackOverflow.Services.Services;

public interface IQuestionService
{
    Task AddAsync(Question question);
    Task<Question> GetByIdAsync(Guid id);
    Task RemoveByIdAsync(Guid id);
    Task UpdateAsync(Question question);
    Task DeleteAsync(Guid questionId);
    Task<(int total, int totalDisplay, IList<Question> records)> GetQuestions(int pageIndex,
            int pageSize, string searchText, string orderBy);
    Task<(int total, int totalDisplay, IList<Question> records)> GetQuestionsByUserId(Guid id, int pageIndex,
            int pageSize, string searchText, string orderBy);
}