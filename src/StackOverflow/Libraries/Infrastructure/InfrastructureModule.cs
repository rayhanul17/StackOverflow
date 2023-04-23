using Autofac;
using NHibernate;
using StackOverflow.Infrastructure.Utility;

namespace StackOverflow.Infrastructure;

public class InfrastructureModule : Module
{
    private readonly string _connectionString;
    public InfrastructureModule(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void Load(ContainerBuilder builder)
    {
        


        base.Load(builder);
    }
}