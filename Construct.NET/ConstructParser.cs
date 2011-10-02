using System.Text;
using System.IO;

namespace Construct.NET
{
    public class ConstructParser : IConstructParser
    {
        public T ParseStream<T>(Stream inputStream, ConstructPlan constructPlan) where T : new()
        {
            var binReader = new BinaryReader(inputStream, Encoding.ASCII);
            var obj = new T();
            foreach (var planAction in constructPlan.PlanActions)
            {
                if (planAction.IsConditionalAction)
                {
                    if (planAction.InvokeConditionalFunction(obj))
                    {
                        planAction.Execute(binReader, obj);
                    }
                }
                else
                {
                    planAction.Execute(binReader, obj);
                }
            }
            return obj;
        }
    }
}