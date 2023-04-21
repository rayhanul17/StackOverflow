using NHibernate;

namespace StackOverflow.DAL.Utility;

public interface IDataSessionFactory
{
    ISession OpenSession();
}
