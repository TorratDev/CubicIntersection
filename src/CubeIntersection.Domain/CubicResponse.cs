namespace CubeIntersection.Domain;

public class CubicResponse
{
    private CubicResponse()
    {
    }

    internal CubicResponse(double intersectedVolume)
    {
        IntersectedVolume = intersectedVolume;
        AreTheyColliding = intersectedVolume > 0;
    }

    public bool AreTheyColliding { get; init; }
    public double IntersectedVolume { get; init; }

    public static CubicResponse Success(double intersectedVolume)
    {
        return new CubicResponse(intersectedVolume);
    }

    public static CubicResponse Failure()
    {
        return new CubicResponse(0);
    }
}