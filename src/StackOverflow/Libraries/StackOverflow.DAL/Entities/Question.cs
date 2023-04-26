namespace StackOverflow.DAL.Entities;

public class Question : BaseEntity, IEntity<Guid>
{
    public virtual string Title { get; set; } = string.Empty;    

}
