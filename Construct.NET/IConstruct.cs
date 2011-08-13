using System.IO;

namespace Construct.NET
{
    public interface IConstruct<T>
    {
        T Parse(Stream inputStream);
        Stream Output(T obj);
        Stream Output(T obj, Stream dest);
    }
}