using FluentNHibernate.AspNetCore.Identity.Mappings;
using StackOverflow.DAL.Entities.Membership;

namespace StackOverflow.DAL.Mapping.Membership;

public class ApplicationUserMap : IdentityUserMapBase<ApplicationUser, Guid>
{
    public ApplicationUserMap() : base(t => t.GeneratedBy.Guid()) // Primary key config
    {
        Map(x => x.FirstName).Not.Nullable();
        Map(x => x.LastName).Not.Nullable();
    }
}
