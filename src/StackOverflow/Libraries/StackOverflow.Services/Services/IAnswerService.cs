using StackOverflow.Services.DTOs;

namespace StackOverflow.Services.Services;

public interface IAnswerService
{
    Task AddAsync(Answer question);
    Task<Answer> GetByIdAsync(Guid id);
    Task RemoveByIdAsync(Guid id);
    Task ApproveByIdAsync(Guid id);
    Task UpdateAsync(Answer question);
    Task DeleteAsync(Guid questionId);
    Task<(int total, int totalDisplay, IList<Answer> records)> GetAnswers(int pageIndex,
            int pageSize, string searchText, string orderBy);
    Task<(int total, int totalDisplay, IList<Answer> records)> GetPendingAnswers(Guid id, int pageIndex,
            int pageSize, string searchText, string orderBy);
    Task<(int total, int totalDisplay, IList<Answer> records)> GetAnswersByQuestion(Guid qid, int pageIndex,
            int pageSize, string searchText, string orderBy);
}