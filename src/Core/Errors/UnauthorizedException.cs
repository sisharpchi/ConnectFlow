using System.Runtime.Serialization;
using Core.Errors;

namespace Core.Core.Errors;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException() { }
    public UnauthorizedException(String message) : base(message) { }
    public UnauthorizedException(String message, Exception inner) : base(message, inner) { }
    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}