using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.Repositories.Membership;

namespace StackOverflow.DAL.UnitOfWorks;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IUserRoleRepository UserRoleRepository { get; }
    IRoleRepository RoleRepository { get; }
    IQuestionRepository QuestionRepository { get; }
    IAnswerRepository AnswerRepository { get; }
}