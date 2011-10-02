namespace Construct.NET.Tests
{
    [Construct]
    class TestStringConstruct
    {
        [ConstructField(1)]
        public string Value { get; set; }
    }
}