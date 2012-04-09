using System;
using System.Text;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class LengthPrefixedStringElementAttribute : Attribute, IConstructElementDescriptor
    {
        public Type LengthType { get; private set; }
        public Encoding TextEncoding { get; private set; }
        public int SerializationOrder { get; private set; }
        public ByteOrder DataByteOrder { get; private set; }

        public LengthPrefixedStringElementAttribute(int serializationOrder, Type elementType, Type lengthType) 
            : this(serializationOrder, elementType, lengthType, Encoding.ASCII)
        {
        }

        public LengthPrefixedStringElementAttribute(int serializationOrder, Type elementType, Type lengthType, Encoding textEncoding)
        {
            textEncoding.RequireNotNull("textEncoding");
            elementType.RequireNotNull("elementType");
            elementType.Require("elementType", t => t.IsConstructable());
            serializationOrder.Require("serializationOrder", arg => arg >= 0);

            SerializationOrder = serializationOrder;
            LengthType = lengthType;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}