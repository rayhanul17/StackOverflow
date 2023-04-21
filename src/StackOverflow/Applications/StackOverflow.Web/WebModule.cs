using Autofac;
using StackOverflow.Web.Areas.Admin.Models;

namespace StackOverflow.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<QuestionModel>().AsSelf();
    }
}
