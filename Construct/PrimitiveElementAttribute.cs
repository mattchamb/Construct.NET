using System;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PrimitiveElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        /// <remarks>Defaults to using <see cref="ByteOrder.Host"/></remarks>
        public PrimitiveElementAttribute(int serializationOrder, Type elementType)
            : this(serializationOrder, elementType, ByteOrder.Host)
        {
        }

        public PrimitiveElementAttribute(int serializationOrder, Type elementType, ByteOrder dataByteOrder)
        {
            Require.NotNull(elementType, "elementType");
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);

            SerializationOrder = serializationOrder;
            DataByteOrder = dataByteOrder;
        }
    }
}