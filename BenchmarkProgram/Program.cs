using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Construct;

namespace BenchmarkProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var planner = new ConstructPlanner();
            var plan = planner.CreatePlan<ExtraLargeData>();
            using (var ms = new MemoryStream())
            {
                plan.WriteConstruct(new ExtraLargeData(), new ConstructWriterStream(ms));
                Console.WriteLine(ms.Length);
            }
        }
    }
}
