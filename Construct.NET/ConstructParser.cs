using System;
using System.IO;
using Construct.NET.Interfaces;

namespace Construct.NET
{
    internal class ConstructParser : IConstructParser
    {
        public T ParseStream<T>(Stream inputStream, ConstructPlan constructPlan) where T : new()
        {
            var binReader = new BinaryReader(inputStream);
            var obj = new T();
            foreach (var planAction in constructPlan.PlanActions)
            {
                planAction.Execute(binReader, obj);
            }
            return obj;
        }
    }
}