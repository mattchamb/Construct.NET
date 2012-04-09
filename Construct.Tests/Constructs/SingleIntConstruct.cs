using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Construct.Tests.Constructs
{
    [Constructable]
    public class SingleIntConstruct
    {
        [PrimitiveElement(1, typeof(int), ByteOrder.Host)]
        public int Integer { get; set; }
    }
}
