using System;
using System.Text;
using Construct.Infrastructure;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class FixedLengthStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public int Length { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }
        public string Condition { get; set; }

        public FixedLengthStringElementAttribute(int serializationOrder, int length) 
            : this(serializationOrder, length, "ASCII")
        {
        }

        public FixedLengthStringElementAttribute(int serializationOrder, int length, string textEncoding)
        {
            Require.NotNullOrEmpty(textEncoding, "textEncoding");
            Require.That("serializationOrder", serializationOrder >= 0);
            Require.That("length", length >= 0);

            SerializationOrder = serializationOrder;
            Length = length;
            TextEncoding = Encoding.GetEncoding(textEncoding);
            DataByteOrder = ByteOrder.None;
        }
    }
}