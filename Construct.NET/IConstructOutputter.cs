using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Construct.NET
{
    public interface IConstructOutputter
    {
        void Output(object obj, BinaryWriter binWriter, ConstructPlan constructPlan);
    }
}
