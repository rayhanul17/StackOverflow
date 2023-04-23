using Autofac;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.UnitOfWorks;
using StackOverflow.DAL.Utility;

namespace StackOverflow.DAL;

public class DALModule : Module
{
    private readonly string _connectionString;

    public DALModule(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<MsSqlSessionFactory>().AsSelf()
        //    .WithParameter("connectionString", _connectionString)
        //    .InstancePerLifetimeScope();

        //builder.RegisterType<MsSqlSessionFactory>().As<IDataSessionFactory>()
        //    .WithParameter("connectionString", _connectionString)
        //    .InstancePerLifetimeScope();

        //builder.Register(c => c.Resolve<MsSqlSessionFactory>().OpenSession()).As<IDataSessionFactory>()
        //    .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}