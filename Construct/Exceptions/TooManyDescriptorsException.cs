using System;
using System.Runtime.Serialization;

namespace Construct.Exceptions
{
    [Serializable]
    public class TooManyDescriptorsException : Exception
    {
        public TooManyDescriptorsException()
        {
        }

        public TooManyDescriptorsException(string message) : base(message)
        {
        }

        public TooManyDescriptorsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TooManyDescriptorsException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
