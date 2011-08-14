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

    [Construct]
    class TestArrayConstruct
    {
        [ConstructField(1)]
        public int Length { get; set; }
        [ConstructField(2)]
        public TestIntConstruct[] Values { get; set; }
    }

    [Flags]
    public enum TestEnum : byte
    {
        Node = 0,
        Test = 1,
        Test2 = 2,
        Test3 = 4
    }

    [Construct]
    class TestEnumConstruct
    {
        [ConstructField(1)]
        public TestEnum Value { get; set; }
    }
}
