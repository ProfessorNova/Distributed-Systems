using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Externalize connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                      ?? throw new InvalidOperationException("Connection string not configured.");

// 2. Register DbContext with Npgsql provider
builder.Services.AddDbContext<ShoppingContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Enable CORS (allow all for lab/demo)
builder.Services.AddCors(op => op.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Apply EnsureCreated at startup (no migrations)
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
