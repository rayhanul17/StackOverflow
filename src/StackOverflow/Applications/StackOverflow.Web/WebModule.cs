﻿using Autofac;
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
        builder.RegisterType<GetQuestionsModel>().AsSelf();
        builder.RegisterType<QuestionEditModel>().AsSelf();
        builder.RegisterType<AnswerModel>().AsSelf();
        builder.RegisterType<GetAnswersModel>().AsSelf();
        builder.RegisterType<AnswerEditModel>().AsSelf();

        base.Load(builder);
    }
}
