using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Construct.NET
{
    public class Construct<T> : IConstruct<T>
    {
        public Construct(IConstructPlanner constructPlanner, IConstructParser constructParser, IConstructOutputter constructOutputter)
        {
            Parser = constructParser;
            Planner = constructPlanner;
            Outputter = constructOutputter;
            Plan = Planner.CreateConstructPlan(typeof (T));
        }

        public ConstructPlan Plan { get; private set; }
        public IConstructParser Parser { get; private set; }
        public IConstructPlanner Planner { get; private set; }
        public IConstructOutputter Outputter { get; private set; }

        public T Parse(Stream inputStream)
        {
            return Parser.ParseStream<T>(inputStream, Plan);
        }

        public Stream Output(T obj)
        {
            return Output(obj, new MemoryStream());
        }

        public Stream Output(T obj, Stream dest)
        {
            var binWriter = new BinaryWriter(dest);
            Outputter.Output(obj, binWriter, Plan);
            return dest;
        }
    }
}
