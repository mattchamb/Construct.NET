using System.IO;

namespace Construct.NET
{
    public interface IConstructOutputter
    {
        void Output(object obj, BinaryWriter binWriter, ConstructPlan constructPlan);
    }
}
