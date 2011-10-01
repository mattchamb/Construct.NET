using System.IO;

namespace Construct.NET.Interfaces
{
    internal interface IConstructParser
    {
        T ParseStream<T>(Stream inputStream, ConstructPlan constructPlan);
    }
}