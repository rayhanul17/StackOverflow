﻿using Autofac;
using StackOverflow.Services.Services.Membership;
using StackOverflow.Web.Areas.Admin.Models;
using StackOverflow.Web.Models;

namespace StackOverflow.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LoginModel>().AsSelf();
        builder.RegisterType<RegisterModel>().AsSelf();
        builder.RegisterType<LogoutModel>().AsSelf();
        builder.RegisterType<QuestionModel>().AsSelf();

        base.Load(builder);
    }
}
