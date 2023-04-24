using Autofac;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.UnitOfWorks;
using StackOverflow.DAL.Utility;
using StackOverflow.Services.Services;
using StackOverflow.Services.Services.Membership;

namespace StackOverflow.Services;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MsSqlSessionFactory>().As<IDataSessionFactory>()
                .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<TimeService>().As<ITimeService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QuestionService>().As<IQuestionService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AccountService>().As<IAccountService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserRoleManager>().As<IUserRoleManager>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>()
            .SingleInstance();

        base.Load(builder);
    }
}
