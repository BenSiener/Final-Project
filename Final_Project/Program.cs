using Microsoft.EntityFrameworkCore;
using Final_Project.Data; // Add this to reference the correct namespace for DatabaseContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Update this to reference the correct DatabaseContext class
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // For Swagger

// Add NSwag
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Final_Project API";
    config.Description = "API documentation for the Final_Project Web API.";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

