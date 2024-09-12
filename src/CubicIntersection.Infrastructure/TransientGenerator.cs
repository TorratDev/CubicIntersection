using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Infrastructure;

public class TransientGenerator : IGenerator
{
    public Result<string, ErrorMessage> GetRandom()
    {
        var nextRandom = Random.Shared.Next(5);

        return nextRandom switch
        {
            2 => ErrorMessage.Prohibited("prohibited message"),
            3 => ErrorMessage.Generic("generic message"),
            _ => Guid.NewGuid().ToString()
        };
    }
}