using System.Runtime.Serialization;

namespace CubicIntersection.Domain;

[DataContract]
public record ScopedResponse
{
    [DataMember(Order = 1)] public bool HasError => Error is null;
    [DataMember(Order = 2)] public ErrorMessage Error { get; set; }

    [DataMember(Order = 3)] public string RandomString { get; set; }

    public static ScopedResponse Success(string random)
    {
        return new ScopedResponse()
        {
            RandomString = random
        };
    }

    public static ScopedResponse EmptyError()
    {
        return new ScopedResponse()
        {
            Error = ErrorMessage.Empty()
        };
    }

    public static ScopedResponse GenericError(string message)
    {
        return new ScopedResponse()
        {
            Error = ErrorMessage.Generic(message)
        };
    }

    public static ScopedResponse GenericProhibited(string message)
    {
        return new ScopedResponse()
        {
            Error = ErrorMessage.Prohibited(message)
        };
    }
}