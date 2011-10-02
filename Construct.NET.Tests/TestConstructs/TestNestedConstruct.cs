namespace Construct.NET.Tests
{
    [Construct]
    class TestNestedConstruct
    {
        [ConstructField(1)]
        public int First { get; set; }
        [ConstructField(2)]
        public TestIntConstruct Nested { get; set; }
    }
}