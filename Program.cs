using Hair_Care_Store.Repositories;
using Hair_Care_Store.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductDapperRepository>();
var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();






app.Run();
