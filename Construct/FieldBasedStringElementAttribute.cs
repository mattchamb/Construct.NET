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
            textEncoding.RequireNotNull("textEncoding");
            elementType.RequireNotNull("elementType");
            elementType.Require("elementType", t => t.IsConstructable());
            serializationOrder.Require("serializationOrder", arg => arg >= 0);
            propertyName.RequireNotNull("propertyName");

            SerializationOrder = serializationOrder;
            PropertyName = propertyName;
            TextEncoding = textEncoding;
            DataByteOrder = ByteOrder.None;
        }
    }
}