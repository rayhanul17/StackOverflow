namespace StackOverflow.DAL.Entities;

public class Answer : BaseEntity
{
    public virtual string Description { get; set; } = string.Empty;
    public virtual Guid QuestionId { get; set; }   
    public virtual bool IsApproved { get; set; }
}
