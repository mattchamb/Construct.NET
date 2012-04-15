namespace Construct.Infrastructure
{
    public interface IConstructPlanner
    {
        ConstructPlan<TConstructable> CreatePlan<TConstructable>() where TConstructable : new();
    }
}