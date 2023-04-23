namespace StackOverflow.Services.DTOs;

public class Answer
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }
    public int VoteCount { get; set; }
}
