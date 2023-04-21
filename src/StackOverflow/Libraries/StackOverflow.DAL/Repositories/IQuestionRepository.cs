using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories;

public interface IQuestionRepository : IRepository<Question, Guid>
{

}