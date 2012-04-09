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
            elementType.RequireNotNull("elementType");
            elementType.Require("elementType", t => t.IsConstructable());
            serializationOrder.Require("serializationOrder", arg => arg >= 0);

            SerializationOrder = serializationOrder;
        }
    }
}