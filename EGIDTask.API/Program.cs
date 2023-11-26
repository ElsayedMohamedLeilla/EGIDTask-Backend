using EGIDTask.API;
using EGIDTask.API.MiddleWares;
using EGIDTask.BusinessLogic;
using EGIDTask.Data;
using EGIDTask.Data.UnitOfWork;
using EGIDTask.Models.AutoMapper;
using EGIDTask.Repository;
using EGIDTask.Repository.RepositoryManager;
using EGIDTask.Validation;
using EGIDTask.Validation.FluentValidation;
using FluentValidation;
using Glamatek.Real_Time.SignalR;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("EGIDTaskConnectionString") ??
    throw new InvalidOperationException("Connection String Not Found");
string AllowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Serilog.Core.Logger logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed((x) => true)
        .AllowCredentials());
});

builder.Services.AddSignalR();

IConfigurationSection appSettingsSection = builder.Configuration.GetSection("AppSettings");

builder.Services.ConfigureSQLContext(builder.Configuration);
builder.Services.ConfigureRepositoryContainer();
builder.Services.ConfigureBLValidation();
builder.Services.ConfigureRepository();
builder.Services.ConfigureBusinessLogic();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureBackGroundService();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.UseCamelCasing(true);
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
});

builder.Services.AddValidatorsFromAssemblyContaining<GetGenaricValidator>();
builder.Services.AddFluentValidationAutoValidation(cpnfig =>
{
    cpnfig.OverrideDefaultResultFactoryWith<FluentValidationResultFactory>();

});

builder.Services.AddAutoMapper((serviceProvider, config) =>
{
}, typeof(OrderMapProfile));

WebApplication app = builder.Build();
IServiceScope serviceScope = app.Services.GetService<IServiceScopeFactory>()
    .CreateScope();

ApplicationDBContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDBContext>();


if (app.Environment.IsDevelopment())
{
    context.Database.Migrate();
}
SeedDB.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
IServiceScopeFactory serviceScopeFactory = serviceScope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
IServiceProvider serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
IUnitOfWork<ApplicationDBContext> unitOfWork = serviceProvider.GetService<IUnitOfWork<ApplicationDBContext>>();
RepositoryManager repositoryManager = new(unitOfWork);

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseStaticFiles();

List<CultureInfo> supportedCultures = new()
{
                    new CultureInfo("en"),
                    new CultureInfo("ar")
                };

app.UseCors(AllowSpecificOrigins);

RequestLocalizationOptions requestLocalizationOptions = new()
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(requestLocalizationOptions);

app.UseMiddleware<ExceptionHandlerMiddleware>();


app.MapControllers();
app.MapHub<SignalRHub>("/realtime");

app.Run();

