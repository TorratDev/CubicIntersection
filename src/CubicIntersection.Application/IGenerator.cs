using CubicIntersection.Domain;

namespace CubicIntersection.Application;

public interface IGenerator
{
    public Result<string, ErrorMessage> GetRandom();
}