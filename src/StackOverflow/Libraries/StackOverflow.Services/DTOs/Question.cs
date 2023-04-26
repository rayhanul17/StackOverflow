namespace StackOverflow.Services.DTOs;

public class Question
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime TimeStamp { get; set; }
    public List<Answer>? Answers { get; set; }
}
