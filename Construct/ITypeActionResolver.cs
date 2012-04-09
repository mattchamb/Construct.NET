using System;

namespace Construct
{
    public interface ITypeActionResolver
    {
        PlanAction<TConstructable> ResolveActionForType<TConstructable>(PropertyConstructDescriptor descriptor);
    }
}