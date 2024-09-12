using System.Runtime.Serialization;

namespace CubicIntersection.Domain;

[DataContract]
public record AlternativeResponse
{
    [DataMember(Order = 1)] public bool HasError => Error is null;
    [DataMember(Order = 2)] public ErrorMessage Error { get; set; }

    [DataMember(Order = 3)] public string RandomString { get; set; }

    public static AlternativeResponse Success(string random)
    {
        return new AlternativeResponse()
        {
            RandomString = random
        };
    }

    public static AlternativeResponse Failure(ErrorMessage errorMessage)
    {
        return new AlternativeResponse()
        {
            Error = errorMessage
        };
    }

    public static AlternativeResponse EmptyError()
    {
        return new AlternativeResponse()
        {
            Error = ErrorMessage.Empty()
        };
    }

    public static AlternativeResponse GenericError(string message)
    {
        return new AlternativeResponse()
        {
            Error = ErrorMessage.Generic(message)
        };
    }

    public static AlternativeResponse ProhibitedError(string message)
    {
        return new AlternativeResponse()
        {
            Error = ErrorMessage.Prohibited(message)
        };
    }
}