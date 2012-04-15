using System;
using System.Runtime.Serialization;

namespace Construct.Exceptions
{
    [Serializable]
    public class ConditionalBindingException : Exception
    {
        public ConditionalBindingException()
        {
        }

        public ConditionalBindingException(string message) : base(message)
        {
        }

        public ConditionalBindingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConditionalBindingException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
