using Microsoft.EntityFrameworkCore;
using SalesOrderProfescipta.Server;
using SalesOrderProfescipta.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SalesDbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("Db")
));

builder.Services.AddTransient<SalesOrderService>();
builder.Services.AddTransient<DropdownService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Change the origins with your localhost server
app.UseCors(builder =>
  builder
    .WithOrigins("https://localhost:57976")
    .WithExposedHeaders("Content-Disposition"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
