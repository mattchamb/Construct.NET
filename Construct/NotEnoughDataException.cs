using System;
using System.Runtime.Serialization;

namespace Construct
{
    [Serializable]
    public class NotEnoughDataException : Exception
    {
        public NotEnoughDataException()
        {
        }

        public NotEnoughDataException(string message) : base(message)
        {
        }

        public NotEnoughDataException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotEnoughDataException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
