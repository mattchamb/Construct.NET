using System.Reflection;

namespace Construct
{
    public class PropertyConstructDescriptor
    {
        public PropertyInfo Property { get; private set; }
        public IConstructElementDescriptor Descriptor { get; private set; }

        public PropertyConstructDescriptor(PropertyInfo property, IConstructElementDescriptor descriptor)
        {
            property.RequireNotNull("property");
            descriptor.RequireNotNull("descriptor");

            Property = property;
            Descriptor = descriptor;
        }
    }
}
