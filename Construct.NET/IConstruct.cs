using System.IO;

namespace Construct.NET
{
    public interface IConstruct<out T>
    {
        T Parse(Stream inputStream);
    }
}