namespace StackOverflow.DAL.Entities;

public class Answer : BaseEntity, IEntity<Guid>
{
    public virtual Guid Id { get; set; }
    public virtual string Description { get; set; } = string.Empty;
    public virtual Guid QuestionId { get; set; }
    public virtual Question Question { get; set; }
}
