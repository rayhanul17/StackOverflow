using NHibernate;
using StackOverflow.DAL.Entities.Membership;

namespace StackOverflow.DAL.Repositories.Membership;

public class RoleRepository : Repository<AppRole, Guid>, IRoleRepository
{
    public RoleRepository(ISession session) : base(session)
    {

    }
}
