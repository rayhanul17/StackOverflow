using Autofac;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.UnitOfWorks;

namespace StackOverflow.Services;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<QuestionService>().As<IQuestionService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();
        builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}
