using System;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PrimitiveConstructElementDescriptor : Attribute, IConstructElementDescriptor
    {
        public Type ElementType { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        /// <remarks>Defaults to using <see cref="ByteOrder.Host"/></remarks>
        public PrimitiveConstructElementDescriptor(int serializationOrder, Type elementType)
            : this(serializationOrder, elementType, ByteOrder.Host)
        {
        }

        public PrimitiveConstructElementDescriptor(int serializationOrder, Type elementType, ByteOrder dataByteOrder)
        {
            elementType.RequireNotNull("elementType");
            serializationOrder.Require("serializationOrder", arg => arg >= 0);

            SerializationOrder = serializationOrder;
            ElementType = elementType;
            DataByteOrder = dataByteOrder;
        }
    }
}