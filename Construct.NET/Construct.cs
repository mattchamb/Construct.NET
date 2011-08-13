using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Construct.NET
{
    public class Construct<T> : IConstruct<T>
    {
        public Construct(IConstructPlanner constructPlanner, IConstructParser constructParser)
        {
            Parser = constructParser;
            Planner = constructPlanner;

            Plan = Planner.CreateConstructPlan(typeof (T));
        }

        public ConstructPlan Plan { get; private set; }
        public IConstructParser Parser { get; private set; }
        public IConstructPlanner Planner { get; private set; }

        public T Parse(Stream inputStream)
        {
            return default(T);
        }

    }
}
