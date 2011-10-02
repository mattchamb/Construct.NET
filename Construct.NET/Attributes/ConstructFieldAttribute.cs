using System;

namespace Construct.NET
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ConstructFieldAttribute : Attribute
    {
        public int SerializationOrder { get; private set; }
        public NestingType Nesting { get; private set; }

        private int _stringLength;
        /// <summary>
        /// Used for fixed length strings.
        /// </summary>
        public int StringLength
        {
            get { return _stringLength; }
            set
            {
                if(value <= 0)
                    throw new ArgumentException("Cannot set a zero or negative fixed string length.");
                _stringLength = value;
            }
        }

        /// <summary>
        /// The name of the condition function.
        /// It is an instance method of the construct
        /// </summary>
        public string ConditionFunction { get; set; }


        public ConstructFieldAttribute(int serializationOrder)
        {
            SerializationOrder = serializationOrder;
            Nesting = NestingType.Inline; //Only support this for now, but it's here for future use.
        }
    }
}