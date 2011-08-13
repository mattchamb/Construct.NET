using System;
using System.IO;

namespace Construct.NET
{
    internal class ConstructParser : IConstructParser
    {
        public T ParseStream<T>(Stream inputStream, ConstructPlan constructPlan)
        {
            using (var binReader = new BinaryReader(inputStream))
            {
                var obj = (T)Activator.CreateInstance(typeof (T));
                foreach (var planAction in constructPlan.PlanActions)
                {
                    planAction.Execute(binReader, obj);
                }
                return obj;
            }
        }
    }
}