using FluentNHibernate.Mapping;
using StackOverflow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Mappings;

public class AnswerMap : ClassMap<Answer>
{
    public AnswerMap()
    {
        Table("Answers");
        Id(x => x.Id);
        Map(x => x.Description);
        Map(x => x.VoteCount);
        References(x => x.Question).Columns("QuestionId").LazyLoad();

    }
}
