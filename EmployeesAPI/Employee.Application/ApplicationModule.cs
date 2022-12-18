using Autofac;
using Employee.Application.Services;
using Employee.Application.Interfaces.Services;

namespace Employee.Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RegionService>().As<IRegionService>();
        builder.RegisterType<EmployeeService>().As<IEmployeeService>();
    }
}