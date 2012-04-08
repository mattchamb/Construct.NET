using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Construct.Tests.Constructs
{
    [Constructable]
    public class SingleIntConstruct
    {
        [PrimitiveConstructElementDescriptor(1, typeof(int))]
        public int Integer { get; set; }
    }
}
