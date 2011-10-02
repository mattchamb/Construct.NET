using System.IO;

namespace Construct.NET
{
    public interface IConstructParser
    {
        T ParseStream<T>(Stream inputStream, ConstructPlan constructPlan) where T : new();
    }
}