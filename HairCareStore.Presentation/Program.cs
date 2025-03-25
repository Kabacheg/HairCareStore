using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Infrastructure.Repositories;
using Hair_Care_Store.Core.Repositories;
using HairCareStore.Infrastructure.Options;
using HairCareStore.Core.Repositories;
using HairCareStore.Infrastructure.Repositories.Dapper_Repositories;
using HairCareStore.Core.Services;
using HairCareStore.Infrastructure.Services;
using Hair_Care_Store.Infrastructure.Repositories.Dapper_Repositories;
using FluentValidation;
using System.Reflection;
using HairCareStore.Core.Validators;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductDapperRepository>();
builder.Services.AddScoped<ITutorialsRepository, TutorialDapperRepository>();
builder.Services.AddScoped<IHttpLogger, HttpLogger>();
builder.Services.AddScoped<IHttpLogRepository, HttpLogDapperRepository>();

builder.Services.AddValidatorsFromAssemblies(new Assembly[] {
    Assembly.GetExecutingAssembly(),
});

builder.Services.Configure<DatabaseOptions>(options => {
    var connectionString = builder.Configuration.GetConnectionString("MyDatabase");

    options.ConnectionString = connectionString!;
});

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
