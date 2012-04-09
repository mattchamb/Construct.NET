using System;
using Construct.Actions;

namespace Construct
{
    public interface ITypeActionResolver
    {
        PlanAction<T> ResolveActionForType<T>(PropertyConstructDescriptor descriptor);
    }

    public class DefaultTypeActionResolver : ITypeActionResolver
    {
        public PlanAction<T> ResolveActionForType<T>(PropertyConstructDescriptor descriptor)
        {
            return new Int32Action<T>(descriptor.Property, descriptor.Descriptor.DataByteOrder);
        }
    }
}