using FluentNHibernate.Mapping;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Mappings;

public class QuestionMap : ClassMap<Question>
{
    public QuestionMap()
    {
        Table("Questions");
        Id(x => x.Id);
        Map(x => x.Title);
        Map(x => x.VoteCount);
        Map(x => x.TimeStamp);        
    }
}
