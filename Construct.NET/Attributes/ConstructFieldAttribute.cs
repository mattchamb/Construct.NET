using System;

namespace Construct.NET
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ConstructFieldAttribute : Attribute
    {
        public int SerializationOrder { get; private set; }

        public NestingType Nesting { get; private set; }

        public ConstructFieldAttribute(int serializationOrder)
        {
            SerializationOrder = serializationOrder;
            Nesting = NestingType.Inline;
        }
    }
}