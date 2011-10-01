using System.IO;

namespace Construct.NET.Interfaces
{
    internal interface IConstructOutputter
    {
        void Output(object obj, BinaryWriter binWriter, ConstructPlan constructPlan);
    }
}
