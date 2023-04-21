namespace StackOverflow.Services.BusinessObjects;

public class Question
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public List<Answer>? Answers { get; set; }
}
