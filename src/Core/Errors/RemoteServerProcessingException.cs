using System.Runtime.Serialization;
namespace ToDoList.Errors;

[Serializable]
public class RemoteServerProcessingException : BaseException
{
    public RemoteServerProcessingException() { }
    public RemoteServerProcessingException(String message) : base(message) { }
    public RemoteServerProcessingException(String message, Exception inner) : base(message, inner) { }
    protected RemoteServerProcessingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}