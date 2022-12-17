using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeAPI;
using EmployeeAPI.Application;
using EmployeeAPI.Infrastructure.DataBase;
using EmployeeAPI.Infrastructure.DataBase.Initializer;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder
        =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new RepositoryModule());
        containerBuilder.RegisterModule(new APIHostModule());
    });

builder.Services.AddDbContext<DataBaseCtx>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!)
        .EnableSensitiveDataLogging(true));

// Add services to the container.
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.ToString()); });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();

if (bool.Parse(builder.Configuration["ShouldSeedDataBase"]!))
{
    CreateDbIfNotExists(app);
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

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