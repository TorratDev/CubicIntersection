using CubicIntersection.Api;
using CubicIntersection.Domain;
using CubicIntersection.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CubicIntersection.Application;

public class PipelineTransientAndScoped : IPipelineTransientAndScoped
{
    private readonly IGenerator _generatorScoped;

    public PipelineTransientAndScoped([FromKeyedServices("Scoped")] IGenerator generatorScoped)
    {
        _generatorScoped = generatorScoped;
    }

    public AlternativeResponse Run()
    {
        var resultScoped = _generatorScoped.GetRandom();

        return resultScoped.Match(
            AlternativeResponse.Success,
            failure =>
            {
                return failure.Type switch
                {
                    ErrorType.Prohibited => AlternativeResponse.ProhibitedError(failure.Message),
                    ErrorType.Generic => AlternativeResponse.GenericError(failure.Message),
                    ErrorType.Empty => AlternativeResponse.EmptyError(),
                    _ => AlternativeResponse.Failure(failure)
                };
            }
        );
    }
}