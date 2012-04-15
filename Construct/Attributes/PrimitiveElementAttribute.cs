using System;
using Construct.Infrastructure;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PrimitiveElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }
        public string Condition { get; set; }

        /// <remarks>Defaults to using <see cref="ByteOrder.Host"/></remarks>
        public PrimitiveElementAttribute(int serializationOrder)
            : this(serializationOrder, ByteOrder.Host)
        {
        }

        public PrimitiveElementAttribute(int serializationOrder, ByteOrder dataByteOrder)
        {
            Require.That("serializationOrder", serializationOrder >= 0);

            SerializationOrder = serializationOrder;
            DataByteOrder = dataByteOrder;
        }
    }
}