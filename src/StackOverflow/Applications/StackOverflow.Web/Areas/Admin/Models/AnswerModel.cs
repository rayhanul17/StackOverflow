namespace StackOverflow.Web.Areas.Admin.Models;

public class AnswerModel
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int VoteCount { get; set; }

    public AnswerModel()
    {
        
    }
}
