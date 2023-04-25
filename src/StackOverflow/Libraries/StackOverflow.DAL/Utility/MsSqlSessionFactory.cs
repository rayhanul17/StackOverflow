using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using StackOverflow.DAL.Mappings;

namespace StackOverflow.DAL.Utility;

public class MsSqlSessionFactory : IDataSessionFactory
{
    public ISessionFactory Session { get; }

    public MsSqlSessionFactory(string connectionString)
    {
        Session = Fluently
            .Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MappingReference>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
    }

    public ISession OpenSession()
    {
        return Session.OpenSession();
    }
}
