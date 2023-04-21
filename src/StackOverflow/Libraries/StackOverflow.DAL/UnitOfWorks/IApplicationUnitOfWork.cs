using StackOverflow.DAL.Repositories;

namespace StackOverflow.DAL.UnitOfWorks;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IQuestionRepository QuestionRepository { get; }
}