using System;
using System.Text;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class FixedLengthStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int Length { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public FixedLengthStringElementAttribute(int serializationOrder, Type elementType, int length) 
            : this(serializationOrder, elementType, length, Encoding.ASCII)
        {
        }

        public FixedLengthStringElementAttribute(int serializationOrder, Type elementType, int length, Encoding textEncoding)
        {
            Require.NotNull(textEncoding, "textEncoding");
            Require.NotNull(elementType, "elementType");
            Require.That(elementType, "elementType", elementType.IsConstructable());
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);
            Require.That(length, "length", length >= 0);

            SerializationOrder = serializationOrder;
            Length = length;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}