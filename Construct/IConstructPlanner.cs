namespace Construct
{
    public interface IConstructPlanner<TConstructable> 
        where TConstructable : new()
    {
        ConstructPlan<TConstructable> CreatePlan();
    }
}