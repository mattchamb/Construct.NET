namespace Construct.NET.Tests
{
    [Construct]
    class TestDoubleConstruct
    {
        [ConstructField(1)]
        public double Value { get; set; }
    }
}