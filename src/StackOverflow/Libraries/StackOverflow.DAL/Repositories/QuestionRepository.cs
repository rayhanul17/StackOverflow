using NHibernate;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories;

public class QuestionRepository : Repository<Question, Guid>, IQuestionRepository
{
    public QuestionRepository(ISession session) : base(session)
    {

    }
}
