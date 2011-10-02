using System.IO;

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

        public static T Parse<T>(Stream inputStream) where T : new()
        {
            return Parser.ParseStream<T>(inputStream, CreatePlan<T>());
        }

        public static byte[] Output<T>(T obj)
        {
            using (var destStream = new MemoryStream())
            {
                var binWriter = new BinaryWriter(destStream);
                Outputter.Output(obj, binWriter, CreatePlan<T>());
                destStream.Seek(0, SeekOrigin.Begin);
                var result = new byte[destStream.Length];
                destStream.Read(result, 0, result.Length);
                return result;
            }
        }

        private static ConstructPlan CreatePlan<T>()
        {
            return Planner.CreateConstructPlan(typeof(T));
        }
    }
}
