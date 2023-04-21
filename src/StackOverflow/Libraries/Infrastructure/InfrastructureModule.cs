using Autofac;
using NHibernate;
using StackOverflow.Infrastructure.Utility;

namespace StackOverflow.Infrastructure;

public class InfrastructureModule : Module
{
    private readonly string _connectionString;
    public InfrastructureModule(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MsSqlSessionFactory>().AsSelf()
            .WithParameter("connectionString", _connectionString)
            .InstancePerLifetimeScope();

        builder.RegisterType<MsSqlSessionFactory>().As<IDataSessionFactory>()
            .WithParameter("connectionString", _connectionString)
            .InstancePerLifetimeScope();

        builder.Register(c => c.Resolve<MsSqlSessionFactory>().OpenSession()).As<ISession>()
            .InstancePerLifetimeScope();


        base.Load(builder);
    }
}