using StackOverflow.Services.DTOs;

namespace StackOverflow.Services.Services;

public interface IQuestionService
{
    Task AddAsync(Question question);
    Task DeleteAsync(Guid questionId);
    Task<(int total, int totalDisplay, IList<Question> records)> GetQuestionsAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
}