using StackOverflow.Services.DTOs;

namespace StackOverflow.Services.Services;

public interface IQuestionService
{
    Task AddAsync(Question question);
    Task UpdateAsync(Question question);
    Task DeleteAsync(Guid questionId);
    Task<(int total, int totalDisplay, IList<Question> records)> GetQuestions(int pageIndex,
            int pageSize, string searchText, string orderBy);
}