namespace StackOverflow.DAL.Entities.Membership;

public class UserRole : IEntity<int>
{
    public virtual int Id { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    public virtual AppRole Role { get; set; } = null!;
}
