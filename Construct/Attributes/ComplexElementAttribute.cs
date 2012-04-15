using System;
using Construct.Infrastructure;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ComplexElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public string Condition { get; set; }

        public ComplexElementAttribute(int serializationOrder)
        {
            Require.That("serializationOrder", serializationOrder >= 0);
            DataByteOrder = ByteOrder.None;
            SerializationOrder = serializationOrder;
        }
    }
}