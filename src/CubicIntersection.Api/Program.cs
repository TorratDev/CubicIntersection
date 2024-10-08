using CubicIntersection.Api;
using CubicIntersection.Domain;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var http = $"http://0.0.0.0:{port}";
Console.WriteLine(port);
builder.WebHost.UseUrls(http);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false
    )
    ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
    options.AddPolicy("CORSPolicy",
        policyBuilder =>
            policyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
    )
);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "My Cubic Intersection API",
            Version = "v1"
        });
});

builder.Services.AddServices();
builder.Services.AddGrpc();

builder.Configuration.AddAppSettingsConfiguration(builder.Environment);
builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection("Cache"));

var app = builder.Build();

app.UseCors("CORSPolicy");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Cubic Intersection API");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.MapMinimalEndpoints();

// app.MapGrpcService<CubicService>();

await app.RunAsync();

// Test usage
namespace CubicIntersection.Api
{
    public partial class Program
    {
    }
}