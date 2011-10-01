using System.IO;
using Construct.NET.Interfaces;

namespace Construct.NET
{
    public static class Construct
    {
        static Construct()
        {
            Parser = new ConstructParser();
            Planner = new ConstructPlanner();
            Outputter = new ConstructOutputter();
        }

        internal static IConstructParser Parser { get; private set; }
        internal static IConstructPlanner Planner { get; private set; }
        internal static IConstructOutputter Outputter { get; private set; }

        public static T Parse<T>(Stream inputStream)
        {
            return Parser.ParseStream<T>(inputStream, CreatePlan<T>());
        }

        public static Stream Output<T>(T obj)
        {
            return Output(obj, new MemoryStream());
        }

        public static Stream Output<T>(T obj, Stream dest)
        {
            var binWriter = new BinaryWriter(dest);
            Outputter.Output(obj, binWriter, CreatePlan<T>());
            return dest;
        }

        private static ConstructPlan CreatePlan<T>()
        {
            return Planner.CreateConstructPlan(typeof(T));
        }
    }
}
