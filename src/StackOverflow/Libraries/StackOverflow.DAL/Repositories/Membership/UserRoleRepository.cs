using NHibernate;
using StackOverflow.DAL.Entities.Membership;

namespace StackOverflow.DAL.Repositories.Membership;

public class UserRoleRepository : Repository<UserRole, int>, IUserRoleRepository
{
    public UserRoleRepository(ISession session) : base(session)
    {

    }
}
