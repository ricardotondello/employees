using Autofac;
using EmployeeAPI.Application.Interfaces.Repositories;
using EmployeeAPI.Infrastructure.DataBase.Repository;

namespace EmployeeAPI.Infrastructure.DataBase
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegionRepository>().As<IRegionRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
        }
    }
}
