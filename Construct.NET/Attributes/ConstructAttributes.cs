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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ConstructFieldAttribute : Attribute
    {
        public int SerializationOrder { get; set; }
    }
}
