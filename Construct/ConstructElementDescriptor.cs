using System;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ConstructElementDescriptor : Attribute, IConstructElementDescriptor
    {
        public int SerializationOrder { get; private set; }
        public Type ElementType { get; private set; }

        public ConstructElementDescriptor(int serializationOrder, Type elementType)
        {
            elementType.RequireNotNull("elementType");
            serializationOrder.Require("serializationOrder", arg => arg >= 0);

            SerializationOrder = serializationOrder;
            ElementType = elementType;
        }
    }
}
