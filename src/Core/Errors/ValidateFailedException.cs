using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Errors
{
    public class ValidateFailedException : BaseException
    {
        public ValidateFailedException() { }
        public ValidateFailedException(String message) : base(message) { }
        public ValidateFailedException(String message, Exception inner) : base(message, inner) { }
        protected ValidateFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
