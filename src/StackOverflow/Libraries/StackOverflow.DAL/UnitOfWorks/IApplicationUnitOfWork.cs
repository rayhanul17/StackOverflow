using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.Repositories.Membership;

namespace StackOverflow.DAL.UnitOfWorks;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IQuestionRepository QuestionRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IRoleRepository RoleRepository { get; }
}