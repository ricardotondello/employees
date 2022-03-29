using Autofac;
using EmployeeAPI.Application.Interfaces.Services;
using EmployeeAPI.Application.Services;

namespace EmployeeAPI.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegionService>().As<IRegionService>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
        }
    }
}
