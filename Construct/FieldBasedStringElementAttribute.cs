using System;
using System.Text;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class FieldBasedStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public string PropertyName { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public FieldBasedStringElementAttribute(int serializationOrder, Type elementType, string propertyName)
            : this(serializationOrder, elementType, propertyName, Encoding.ASCII)
        {
        }

        public FieldBasedStringElementAttribute(int serializationOrder, Type elementType, string propertyName, Encoding textEncoding)
        {
            Require.NotNull(textEncoding, "textEncoding");
            Require.NotNull(elementType, "elementType");
            Require.That(elementType, "elementType", elementType.IsConstructable());
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);
            Require.NotNull(propertyName, "propertyName");

            SerializationOrder = serializationOrder;
            PropertyName = propertyName;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}