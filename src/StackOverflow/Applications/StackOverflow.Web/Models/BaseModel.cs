using Autofac;
using AutoMapper;

namespace StackOverflow.Web.Models;

public abstract class BaseModel
{
    protected IMapper _mapper = null!;
    protected ILifetimeScope _scope = null!;

    public BaseModel()
    {

    }

    public BaseModel(IMapper mapper, ILifetimeScope scope)
    {
        _mapper = mapper;
        _scope = scope;
    }

    public virtual void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _mapper = _scope.Resolve<IMapper>();
    }
}
