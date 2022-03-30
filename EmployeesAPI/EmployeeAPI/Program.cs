using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeAPI;
using EmployeeAPI.Application;
using EmployeeAPI.Infrastructure.DataBase;
using EmployeeAPI.Infrastructure.DataBase.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder
        =>
    {
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterModule(new RepositoryModule());
        builder.RegisterModule(new APIHostModule());
    });

builder.Services.AddDbContext<DataBaseCtx>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging(true));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});
var app = builder.Build();

CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataBaseCtx>();
        DbInitializer.Initialize(context);
        
    }
}