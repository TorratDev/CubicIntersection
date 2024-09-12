using CubicIntersection.Application;
using CubicIntersection.Application.Wrappers;
using CubicIntersection.Domain;

namespace CubicIntersection.Api;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapMinimalEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/basic", (CubicRequest cubicRequest, BasicWrapper intersectService) =>
        {
            if (intersectService.Intersects(cubicRequest.First, cubicRequest.Second))
            {
                return Results.Ok(
                    CubicResponse.Success(intersectService.IntersectedVolume(cubicRequest.First, cubicRequest.Second))
                );
            }

            return Results.Ok(CubicResponse.Failure());
        });

        builder.MapPost("/api/mirror", (CubicRequest cubicRequest, MirrorWrapper intersectService) =>
        {
            if (intersectService.Intersects(cubicRequest.First, cubicRequest.Second))
            {
                return Results.Ok(
                    CubicResponse.Success(intersectService.IntersectedVolume(cubicRequest.First, cubicRequest.Second))
                );
            }

            return Results.Ok(CubicResponse.Failure());
        });

        builder.MapPost("/api/pipeline", (CubicRequest cubicRequest, IPipeline pipeline) =>
        {
            var response = pipeline.Run(cubicRequest);

            return Results.Ok(response);
        });


        return builder;
    }
}