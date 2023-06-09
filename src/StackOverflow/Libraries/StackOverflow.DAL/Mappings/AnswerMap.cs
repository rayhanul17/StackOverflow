﻿using FluentNHibernate.Mapping;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Mappings;

public class AnswerMap : ClassMap<Answer>
{
    public AnswerMap()
    {
        Table("Answers");
        Id(x => x.Id);
        Map(x => x.Description);
        Map(x => x.VoteCount);
        Map(x => x.TimeStamp);
        Map(x => x.QuestionId);
        Map(x => x.OwnerId);
        Map(x => x.IsApproved);
    }
}
