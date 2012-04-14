using System;
using System.Text;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class FixedLengthStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int Length { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public FixedLengthStringElementAttribute(int serializationOrder, int length) 
            : this(serializationOrder, length, Encoding.ASCII)
        {
        }

        public FixedLengthStringElementAttribute(int serializationOrder, int length, Encoding textEncoding)
        {
            Require.NotNull(textEncoding, "textEncoding");
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);
            Require.That(length, "length", length >= 0);

            SerializationOrder = serializationOrder;
            Length = length;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}