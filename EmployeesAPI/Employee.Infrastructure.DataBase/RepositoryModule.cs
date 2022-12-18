using Autofac;
using Employee.Application.Interfaces.Repositories;
using Employee.Infrastructure.DataBase.Repository;

namespace Employee.Infrastructure.DataBase;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegionRepository>().As<IRegionRepository>();
        builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
    }
}