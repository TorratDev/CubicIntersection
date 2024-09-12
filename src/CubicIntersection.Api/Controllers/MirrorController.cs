using CubicIntersection.Application;
using CubicIntersection.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CubicIntersection.Api.Controllers;

public class MirrorController : Controller
{
    private readonly IIntersectService _intersectService;

    public MirrorController([FromKeyedServices("Mirror")] IIntersectService intersectService)
    {
        _intersectService = intersectService;
    }

    [HttpPost("/controller/mirror")]
    public CubicResponse Post(CubicRequest cubicRequest)
    {
        if (_intersectService.Intersects(cubicRequest.First, cubicRequest.Second))
        {
            return CubicResponse.Success(_intersectService.IntersectedVolume(cubicRequest.First, cubicRequest.Second));
        }

        return CubicResponse.Failure();
    }
}