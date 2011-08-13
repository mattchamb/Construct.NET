using System;

namespace Construct.NET
{
    internal class ConstructPlanner : IConstructPlanner
    {
        public ConstructPlan CreateConstructPlan<T>()
        {
            return CreateConstructPlan(typeof (T));
        }

        public ConstructPlan CreateConstructPlan(Type type)
        {
            throw new NotImplementedException();
        }
    }
}