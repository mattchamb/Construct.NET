namespace Construct.Infrastructure
{
    public interface ITypeActionResolver
    {
        PlanAction<TConstructable> ResolveActionForType<TConstructable>(PropertyConstructDescriptor descriptor);
    }
}