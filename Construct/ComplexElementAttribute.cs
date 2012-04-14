using System;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ComplexElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; set; }

        public ComplexElementAttribute(int serializationOrder, Type elementType)
        {
            Require.NotNull(elementType, "elementType");
            Require.That(elementType, "elementType", elementType.IsConstructable());
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);

            SerializationOrder = serializationOrder;
        }
    }
}