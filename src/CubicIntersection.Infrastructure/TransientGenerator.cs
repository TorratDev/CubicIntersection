using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Infrastructure;

public class TransientGenerator : IGenerator
{
    private readonly string _guid;
    private static readonly int NextRandom = Random.Shared.Next(10);

    public TransientGenerator()
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