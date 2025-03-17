using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories;
using Hair_Care_Store.Repositories.Base;
using HairCareStore.Options;
using HairCareStore.Repositories.Base;
using HairCareStore.Repositories.Dapper_Repositories;
using HairCareStore.Services;
using HairCareStore.Services.Base;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductDapperRepository>();
builder.Services.AddScoped<ITutorialsRepository, TutorialDapperRepository>();
builder.Services.AddScoped<IHttpLogger, HttpLogger>();
builder.Services.AddScoped<IHttpLogRepository, HttpLogDapperRepository>();

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
