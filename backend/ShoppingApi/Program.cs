using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                      ?? throw new InvalidOperationException("Connection string not configured.");

builder.Services.AddDbContext<ShoppingContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(op => op.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShoppingContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();
app.Run();
