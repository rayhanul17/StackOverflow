namespace StackOverflow.DAL.Entities;

public class Answer
{
    public virtual Guid Id { get; set; }
    public virtual string Description { get; set; } = string.Empty;
    public virtual int VoteCount { get; set; }
}
