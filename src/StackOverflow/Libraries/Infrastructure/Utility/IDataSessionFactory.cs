using NHibernate;

namespace StackOverflow.Infrastructure.Utility;

public interface IDataSessionFactory
{
    ISession OpenSession();
}
