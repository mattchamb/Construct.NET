using System;

namespace Construct
{
    public interface IConstructElementDescriptor
    {
        int SerializationOrder { get; }
        Type ElementType { get; }
    }
}