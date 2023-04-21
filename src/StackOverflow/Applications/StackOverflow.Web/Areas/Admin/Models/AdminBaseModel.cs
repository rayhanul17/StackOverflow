using Autofac;

namespace StackOverflow.Web.Areas.Admin.Models;

public class AdminBaseModel
{
    protected ILifetimeScope _scope;
    
    public AdminBaseModel()
    {

    }

    public virtual void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
    }
}
