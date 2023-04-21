using Autofac;
using NHibernate;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.UnitOfWorks;
using StackOverflow.DAL.Utility;

namespace StackOverflow.DAL;

public class DALModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        //builder.RegisterType<MsSqlSessionFactory>().As<ISession>()
        //    .InstancePerLifetimeScope();
        
        builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
            .InstancePerLifetimeScope();

        

        base.Load(builder);
    }
}