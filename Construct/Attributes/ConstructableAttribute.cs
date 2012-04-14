using System;

namespace Construct.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class ConstructableAttribute : Attribute
    {
    }
}
