using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Api;

public class PipelineTransientAndScoped : IPipelineTransientAndScoped
{
    private readonly IGenerator _generatorSingletonFirst;
    private readonly IGenerator _generatorSingletonSecond;
    private readonly IGenerator _generatorScopedFirst;
    private readonly IGenerator _generatorScopedSecond;
    private readonly IGenerator _generatorTransientFirst;
    private readonly IGenerator _generatorTransientSecond;

    public PipelineTransientAndScoped(
        [FromKeyedServices(Extensions.Singleton)]
        IGenerator generatorSingletonFirst,
        [FromKeyedServices(Extensions.Singleton)]
        IGenerator generatorSingletonSecond,
        [FromKeyedServices(Extensions.Scoped)] IGenerator generatorScopedFirst,
        [FromKeyedServices(Extensions.Scoped)] IGenerator generatorScopedSecond,
        [FromKeyedServices(Extensions.Transient)]
        IGenerator generatorTransientFirst,
        [FromKeyedServices(Extensions.Transient)]
        IGenerator generatorTransientSecond)
    {
        _generatorSingletonFirst = generatorSingletonFirst;
        _generatorSingletonSecond = generatorSingletonSecond;
        _generatorScopedFirst = generatorScopedFirst;
        _generatorScopedSecond = generatorScopedSecond;
        _generatorTransientFirst = generatorTransientFirst;
        _generatorTransientSecond = generatorTransientSecond;
    }

    public AlternativeResponse Run()
    {
        var resultTransientFirst = _generatorTransientFirst.GetRandom();

        if (!resultTransientFirst.IsOk)
        {
            return AlternativeResponse.Failure(resultTransientFirst.Error);
        }

        var resultTransientSecond = _generatorTransientSecond.GetRandom();
        if (!resultTransientSecond.IsOk)
        {
            return AlternativeResponse.Failure(resultTransientSecond.Error);
        }

        var resultScopedFirst = _generatorScopedFirst.GetRandom();
        if (!resultScopedFirst.IsOk)
        {
            return AlternativeResponse.Failure(resultScopedFirst.Error);
        }

        var resultScopedSecond = _generatorScopedSecond.GetRandom();
        if (!resultScopedSecond.IsOk)
        {
            return AlternativeResponse.Failure(resultScopedSecond.Error);
        }

        var resultSingletonFirst = _generatorSingletonFirst.GetRandom();
        if (!resultSingletonFirst.IsOk)
        {
            return AlternativeResponse.Failure(resultSingletonFirst.Error);
        }

        var resultSingletonSecond = _generatorSingletonSecond.GetRandom();

        return resultSingletonSecond.Match(
            success => AlternativeResponse.Success(
                $"{resultSingletonFirst.Value}//" +
                $"{success}**" +
                $"{resultScopedFirst.Value}**" +
                $"{resultScopedSecond.Value}++" +
                $"{resultTransientFirst.Value}++" +
                $"{resultTransientSecond.Value}//"),
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