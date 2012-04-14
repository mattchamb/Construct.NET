using System;
using System.Text;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class LengthPrefixedStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public Type LengthType { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public LengthPrefixedStringElementAttribute(int serializationOrder, Type lengthType) 
            : this(serializationOrder, lengthType, Encoding.ASCII)
        {
        }

        public LengthPrefixedStringElementAttribute(int serializationOrder, Type lengthType, Encoding textEncoding)
        {
            Require.NotNull(textEncoding, "textEncoding");
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);

            SerializationOrder = serializationOrder;
            LengthType = lengthType;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}