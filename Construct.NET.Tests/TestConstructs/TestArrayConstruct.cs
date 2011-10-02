namespace Construct.NET.Tests
{
    [Construct]
    class TestArrayConstruct
    {
        [ConstructField(1)]
        public int Length { get; set; }
        [ConstructField(2)]
        public TestIntConstruct[] Values { get; set; }
    }
}