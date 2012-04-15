using System.Reflection;
using Construct.Attributes;
using Construct.Infrastructure;

namespace Construct
{
    public class PropertyConstructDescriptor
    {
        public PropertyInfo Property { get; private set; }
        public IConstructElementDescriptor Descriptor { get; private set; }

        public PropertyConstructDescriptor(PropertyInfo property, IConstructElementDescriptor descriptor)
        {
            Require.NotNull(property, "property");
            Require.NotNull(descriptor, "descriptor");

            Property = property;
            Descriptor = descriptor;
        }
    }
}
