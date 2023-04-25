using FluentNHibernate.Mapping;
using StackOverflow.DAL.Entities.Membership;

namespace StackOverflow.DAL.Mapping.Membership;

public class RoleMap : ClassMap<AppRole>
{
    public RoleMap()
    {
        Table("AspNetRoles");
        Id(x => x.Id).GeneratedBy.Guid();
        Map(x => x.Name);
        Map(x => x.NormalizedName);
    }
}
