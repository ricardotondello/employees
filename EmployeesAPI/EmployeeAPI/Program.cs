using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeeAPI;
using Employee.Application;
using Employee.Infrastructure.DataBase;
using Employee.Infrastructure.DataBase.Initializer;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(
    containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new RepositoryModule());
        containerBuilder.RegisterModule(new APIHostModule());
    });

builder.Services.AddDbContext<DataBaseCtx>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!).EnableSensitiveDataLogging());

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.ToString()); });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    CreateDbIfNotExists(app);
}

if (bool.Parse(builder.Configuration["ShouldSeedDataBase"]!))
{
    SeedDataBase(app);
}

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

static DataBaseCtx CreateDbIfNotExists(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataBaseCtx>();
    context.Database.EnsureCreated();
    return context;
}

static void SeedDataBase(IHost host)
{
    var context = CreateDbIfNotExists(host);
    DbInitializer.Initialize(context);
}