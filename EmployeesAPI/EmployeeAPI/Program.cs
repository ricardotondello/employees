using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeAPI;
using EmployeeAPI.Application;
using EmployeeAPI.Infrastructure.DataBase;
using EmployeeAPI.Infrastructure.DataBase.Initializer;
using Microsoft.EntityFrameworkCore;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataBaseCtx>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}