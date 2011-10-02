namespace Construct.NET.Tests
{
    [Construct]
    class TestEnumConstruct
    {
        [ConstructField(1)]
        public TestByteEnum Value { get; set; }
    }
}