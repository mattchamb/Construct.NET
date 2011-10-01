using System;

namespace Construct.NET.Interfaces
{
    internal interface IConstructPlanner
    {
        ConstructPlan CreateConstructPlan<T>();
        ConstructPlan CreateConstructPlan(Type type);
    }
}