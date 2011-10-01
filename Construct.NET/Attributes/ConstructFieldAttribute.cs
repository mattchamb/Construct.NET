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
        /// The name of the property to pass to the Condition function.
        /// </summary>
        public string ConditionArgument { get; set; }
        /// <summary>
        /// The function that says whether to include the conditional field to which it is attached.
        /// Requires the ConditionArgument property to be set.
        /// </summary>
        public Func<object, bool> Condition { get; set; }

        public ConstructFieldAttribute(int serializationOrder)
        {
            SerializationOrder = serializationOrder;
            Nesting = NestingType.Inline; //Only support this for now, but it's here for future use.
        }
    }
}