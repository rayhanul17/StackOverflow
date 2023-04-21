namespace StackOverflow.DAL.Entities;

public class Question : IEntity<Guid>
{
    public virtual Guid Id { get; set; }
    public virtual string Title { get; set; } = string.Empty;
    public virtual int VoteCount { get; set; }
    public virtual List<Answer>? Answers { get; set; }

}
