using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Construct.NET.Attributes;

namespace Construct.NET.Tests
{
    [Construct]
    class TestConstruct
    {
        [ConstructField(SerializationOrder = 1)]
        public int First { get; set; }

        [ConstructField(SerializationOrder = 2)]
        public int Second { get; set; }
    }
}
