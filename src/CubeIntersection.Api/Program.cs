using CubeIntersection.Application;
using CubeIntersection.Domain;
using CubeIntersection.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "My API",
            Version = "v1"
        });
});

builder.Services.AddScoped<IPipeline, Pipeline>();
builder.Services.AddScoped<IIntersectService, IntersectService>();
builder.Services.AddScoped<IVolumeCalculator, VolumeCalculator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.MapPost("/api/basic", (CubicRequest cubicRequest, IIntersectService intersectService) =>
{
    if (intersectService.Intersects(cubicRequest.First, cubicRequest.Second))
    {
        return Results.Ok(
            CubicResponse.Success(intersectService.IntersectedVolume(cubicRequest.First, cubicRequest.Second))
        );
    }

    return Results.Ok(CubicResponse.Failure());
});

app.MapPost("/api/pipeline", (CubicRequest cubicRequest, IPipeline pipeline) =>
{
    var response = pipeline.Run(cubicRequest);

    return Results.Ok(response);
});


app.Run();