using System.Runtime.Serialization;
namespace Core.Errors;

[Serializable]
public class InvalidStateException : BaseException
{
    public InvalidStateException() { }
    public InvalidStateException(String message) : base(message) { }
    public InvalidStateException(String message, Exception inner) : base(message, inner) { }
    protected InvalidStateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}