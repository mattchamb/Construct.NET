using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Construct.NET.Attributes;

namespace Construct.NET.Tests
{
    [Construct]
    class TestIntConstruct
    {
        [ConstructField(1)]
        public int First { get; set; }

        [ConstructField(2)]
        public int Second { get; set; }
    }

    [Construct]
    class TestDoubleConstruct
    {
        [ConstructField(1)]
        public double Value { get; set; }
    }

    [Construct]
    class TestStringConstruct
    {
        [ConstructField(1)]
        public string Value { get; set; }
    }

    [Construct]
    class TestNestedConstruct
    {
        [ConstructField(1)]
        public int First { get; set; }
        [ConstructField(2)]
        public TestIntConstruct Nested { get; set; }
    }
}
