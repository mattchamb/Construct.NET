using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Construct
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class ConstructableAttribute : Attribute
    {
    }
}
