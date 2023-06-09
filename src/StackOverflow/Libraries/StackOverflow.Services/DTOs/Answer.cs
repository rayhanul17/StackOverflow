﻿namespace StackOverflow.Services.DTOs;

public class Answer
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public DateTime TimeStamp { get; set; }
    public bool IsApproved { get; set; }
    public Guid OwnerId { get; set; }

}
