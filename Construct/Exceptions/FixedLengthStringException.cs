using System;
using System.Runtime.Serialization;

namespace Construct.Exceptions
{
    [Serializable]
    public class FixedLengthStringException : Exception
    {
        public FixedLengthStringException()
        {
        }

        public FixedLengthStringException(string message) : base(message)
        {
        }

        public FixedLengthStringException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FixedLengthStringException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}