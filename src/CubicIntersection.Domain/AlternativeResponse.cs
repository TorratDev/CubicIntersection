using System.Runtime.Serialization;

namespace CubicIntersection.Domain;

[DataContract]
public record AlternativeResponse
{
    [DataMember(Order = 1)] public bool HasError => Error is null;
    [DataMember(Order = 2)] public ErrorMessage Error { get; set; }

    [DataMember(Order = 3)] public ServicesResults ServicesResults { get; set; }

    public static AlternativeResponse Success(IEnumerable<string> singletons, IEnumerable<string> scoped, IEnumerable<string> transients)
    {
        return new AlternativeResponse
        {
            ServicesResults = new ServicesResults(singletons, scoped, transients)
        };
    }

    public static AlternativeResponse Failure(ErrorMessage errorMessage)
    {
        return new AlternativeResponse
        {
            Error = errorMessage
        };
    }

    public static AlternativeResponse EmptyError()
    {
        return new AlternativeResponse
        {
            Error = ErrorMessage.Empty()
        };
    }

    public static AlternativeResponse GenericError(string message)
    {
        return new AlternativeResponse
        {
            Error = ErrorMessage.Generic(message)
        };
    }

    public static AlternativeResponse ProhibitedError(string message)
    {
        return new AlternativeResponse
        {
            Error = ErrorMessage.Prohibited(message)
        };
    }
}

[DataContract]
public record ServicesResults(IEnumerable<string> Singletons, IEnumerable<string> Scoped, IEnumerable<string> Transient);