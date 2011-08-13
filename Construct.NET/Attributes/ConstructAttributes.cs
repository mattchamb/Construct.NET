using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Construct.NET.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class ConstructAttribute : Attribute
    {
    }

    public enum NestingType
    {
        Inline,
        Offset
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ConstructFieldAttribute : Attribute
    {
        public int SerializationOrder { get; private set; }

        public NestingType Nesting { get; private set; }

        public ConstructFieldAttribute(int serializationOrder, NestingType nestingType = NestingType.Inline)
        {
            SerializationOrder = serializationOrder;
            Nesting = nestingType;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConstructTargetAttribute : Attribute
    {
        public Type Target { get; private set; }
        public ConstructTargetAttribute(Type target)
        {
            if(target == null)
                throw new ArgumentNullException("target");
            Target = target;
        }
    }
}
