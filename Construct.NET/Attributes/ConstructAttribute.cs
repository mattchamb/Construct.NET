using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Construct.NET
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class ConstructAttribute : Attribute
    {
    }
}
