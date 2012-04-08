using System;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ComplexConstructElementDescriptor : Attribute, IConstructElementDescriptor
    {
        public Type ElementType { get; private set; }
        public int SerializationOrder { get; private set; }

        public ComplexConstructElementDescriptor(int serializationOrder, Type elementType)
        {
            elementType.RequireNotNull("elementType");
            elementType.Require("elementType", t => t.IsConstructable());
            serializationOrder.Require("serializationOrder", arg => arg >= 0);

            SerializationOrder = serializationOrder;
            ElementType = elementType;
        }
    }
}