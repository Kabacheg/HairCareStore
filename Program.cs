using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories;
using Hair_Care_Store.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductDapperRepository>();
builder.Services.AddScoped<ITutorialsRepository, TutorialDapperRepository>();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
