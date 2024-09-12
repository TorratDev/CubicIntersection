using CubicIntersection.Application;
using CubicIntersection.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CubicIntersection.Infrastructure;

public class ScopedGenerator : IGenerator
{
    private readonly IGenerator _generatorTransient;

    public ScopedGenerator([FromKeyedServices("Transient")] IGenerator generatorTransient)
    {
        _generatorTransient = generatorTransient;
    }

    public Result<string, ErrorMessage> GetRandom()
    {
        var result = _generatorTransient.GetRandom();

        if (!result.IsOk)
        {
            return result.Error;
        }

        var nextRandom = Random.Shared.Next(5);

        return nextRandom switch
        {
            2 => ErrorMessage.Prohibited("prohibited message"),
            3 => ErrorMessage.Generic("generic message"),
            _ => $"{Guid.NewGuid().ToString()}/{result.Value}"
        };
    }
}