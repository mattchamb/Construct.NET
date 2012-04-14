using System;
using System.Text;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class FieldBasedStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public string PropertyName { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public FieldBasedStringElementAttribute(int serializationOrder, string propertyName)
            : this(serializationOrder, propertyName, Encoding.ASCII)
        {
        }

        public FieldBasedStringElementAttribute(int serializationOrder, string propertyName, Encoding textEncoding)
        {
            Require.NotNull(textEncoding, "textEncoding");
            Require.That(serializationOrder, "serializationOrder", serializationOrder >= 0);
            Require.NotNull(propertyName, "propertyName");

            SerializationOrder = serializationOrder;
            PropertyName = propertyName;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}