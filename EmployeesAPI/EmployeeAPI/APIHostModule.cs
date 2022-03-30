using Autofac;
using EmployeeAPI.Controllers;

namespace EmployeeAPI
{
    public class APIHostModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegionController>();
        }
    }
}