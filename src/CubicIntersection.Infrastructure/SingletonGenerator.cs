using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Infrastructure;

public class SingletonGenerator : IGenerator
{
    private readonly IGenerator _generator;
    private static readonly int NextRandom = Random.Shared.Next(10);
    private readonly string _guid;

    public SingletonGenerator()
    {
        _guid = Guid.NewGuid().ToString();
    }

    public Result<string, ErrorMessage> GetRandom()
    {
        return NextRandom switch
        {
            2 => ErrorMessage.Prohibited("prohibited message"),
            3 => ErrorMessage.Generic("generic message"),
            _ => _guid
        };
    }
}