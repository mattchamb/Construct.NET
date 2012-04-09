namespace Construct
{
    public interface IConstructElementDescriptor
    {
        int SerializationOrder { get; }
        ByteOrder DataByteOrder { get; }
    }
}