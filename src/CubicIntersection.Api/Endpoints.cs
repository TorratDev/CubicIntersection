using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Api;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapMinimalEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/basic", (CubicRequest cubicRequest, IIntersectService intersectService) =>
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