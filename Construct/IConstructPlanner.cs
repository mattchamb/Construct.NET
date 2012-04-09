namespace Construct
{
    public interface IConstructPlanner
    {
        ConstructPlan<TConstructable> CreatePlan<TConstructable>() where TConstructable : new();
    }
}