using FluentNHibernate.AspNetCore.Identity;

namespace StackOverflow.DAL.Entities.Membership;

public class Role : IdentityRole<Guid>
{

}

public class AppRole : IEntity<Guid>
{
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; } = null!;
    public virtual string? NormalizedName { get; set; } = null!;
}
