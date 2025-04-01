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
using Microsoft.EntityFrameworkCore;
using HairCareStore.Infrastructure.Data;
using HairCareStore.Infrastructure.Repositories.EntityFrameWorkRepositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductEntityFrameWorkRepository>();
builder.Services.AddScoped<ITutorialsRepository, TutorialEntityFrameWorkRepository>();
builder.Services.AddScoped<IHttpLogger, HttpLogger>();
builder.Services.AddScoped<IHttpLogRepository, HttpLogDapperRepository>();
builder.Services.AddScoped<IUserRepository, UserEntityFrameWorkRepository>();

builder.Services.AddDataProtection();

builder.Services.AddValidatorsFromAssemblies(new Assembly[] {
    typeof(ProductValidator).Assembly,
    Assembly.GetExecutingAssembly(),
});

builder.Services.Configure<DatabaseOptions>(options => {
    var connectionString = builder.Configuration.GetConnectionString("MyDatabase");

    options.ConnectionString = connectionString!;
});

builder.Services.AddDbContext<HairCareStoreDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("MyDatabase");
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme,
        configureOptions: options =>
        {
            options.LoginPath = "/Identity/Login";
        });

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
