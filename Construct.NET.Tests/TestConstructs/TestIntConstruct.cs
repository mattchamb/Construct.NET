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
}
