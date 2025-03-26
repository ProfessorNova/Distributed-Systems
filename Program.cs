var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Enable Swagger/OpenAPI generation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For lab/testing purposes, allow all CORS requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the CORS policy
app.UseCors("AllowAll");

app.MapControllers();

app.Run();