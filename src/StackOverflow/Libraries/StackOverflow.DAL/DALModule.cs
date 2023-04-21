using Autofac;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.UnitOfWorks;
using StackOverflow.DAL.Utility;

namespace StackOverflow.DAL;

public class DALModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MsSqlSessionFactory>().As<IDataSessionFactory>()
                .InstancePerLifetimeScope();
        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();
        builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
            .InstancePerLifetimeScope();
    }
}