namespace Construct.Infrastructure
{
    public interface IConstructElementDescriptor
    {
        int SerializationOrder { get; }
        ByteOrder DataByteOrder { get; }
        string Condition { get; }
    }
}