using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Construct.Attributes;

namespace Construct.Tests.Constructs
{
    [Constructable]
    public class SingleIntConstruct
    {
        [PrimitiveElement(1, ByteOrder.Host)]
        public int Integer { get; set; }
    }
}
