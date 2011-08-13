using System;

namespace Construct.NET
{
    public interface IConstructPlanner
    {
        ConstructPlan CreateConstructPlan<T>();
        ConstructPlan CreateConstructPlan(Type type);
    }
}