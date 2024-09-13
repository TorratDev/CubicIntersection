using System.Runtime.Serialization;

namespace CubicIntersection.Domain;

[DataContract]
public class ErrorMessage
{
    [DataMember] public string Message { get; set; }
    [DataMember] public ErrorType Type { get; set; }

    public static ErrorMessage Empty()
    {
        return new ErrorMessage
        {
            Type = ErrorType.Empty
        };
    }

    public static ErrorMessage Generic(string message)
    {
        return new ErrorMessage
        {
            Message = message,
            Type = ErrorType.Generic
        };
    }

    public static ErrorMessage Prohibited(string message)
    {
        return new ErrorMessage
        {
            Message = message,
            Type = ErrorType.Prohibited
        };
    }
}