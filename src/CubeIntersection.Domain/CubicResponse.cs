using System.Runtime.Serialization;

namespace CubeIntersection.Domain;

[DataContract]
public record CubicResponse
{
    public CubicResponse()
    {
    }

    internal CubicResponse(double intersectedVolume)
    {
        IntersectedVolume = intersectedVolume;
        AreTheyColliding = intersectedVolume > 0;
    }

    [DataMember(Order = 1)] public bool AreTheyColliding { get; set; }

    [DataMember(Order = 2)] public double IntersectedVolume { get; set; }

    public static CubicResponse Success(double intersectedVolume)
    {
        return new CubicResponse(intersectedVolume);
    }

    public static CubicResponse Failure()
    {
        return new CubicResponse(0);
    }
}