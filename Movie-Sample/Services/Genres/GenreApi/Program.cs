using GenreApi.Models;
using GenreApi.Models.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IGenreRepository, GenreRepository>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddApiVersioning(config => 
{
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddDbContext<GenreDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GenreDbContext"));
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("Serilog.json")
    .Build();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();


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

try
{
    Log.Information("Application starting...");
    app.Run();
}
catch (Exception)
{
    Log.Error("Error running application");
}
finally
{
    Log.CloseAndFlush();
}


