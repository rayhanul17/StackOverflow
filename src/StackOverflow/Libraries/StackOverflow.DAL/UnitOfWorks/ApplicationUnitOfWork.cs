using NHibernate;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.Repositories.Membership;

namespace StackOverflow.DAL.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
{
    public ApplicationUnitOfWork(ISession session) : base(session)
    {
        QuestionRepository = new QuestionRepository(session);
        UserRoleRepository = new UserRoleRepository(session);
        RoleRepository = new RoleRepository(session);
    }

    public IQuestionRepository QuestionRepository { get; private set; }
    public IUserRoleRepository UserRoleRepository { get; private set; }
    public IRoleRepository RoleRepository { get; private set; }
}
