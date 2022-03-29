using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeAPI;
using EmployeeAPI.Application;
using EmployeeAPI.Infrastructure.DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder
        =>
    {
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterModule(new RepositoryModule());
        builder.RegisterModule(new APIHostModule());
    });

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
