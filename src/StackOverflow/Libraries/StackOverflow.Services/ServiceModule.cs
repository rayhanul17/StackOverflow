using Autofac;

namespace StackOverflow.Services;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<QuestionService>().As<IQuestionService>()
            .InstancePerLifetimeScope();
    }
}
