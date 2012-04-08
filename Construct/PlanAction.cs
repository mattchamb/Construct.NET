using System.Reflection;

namespace Construct
{
    public abstract class PlanAction<TConstructable>
    {
        public PropertyInfo Property { get; private set; }
        public ByteOrder InputByteOrder { get; private set; }

        protected PlanAction(PropertyInfo property, ByteOrder inputByteOrder)
        {
            property.RequireNotNull("property");
            Property = property;
            InputByteOrder = inputByteOrder;
        }

        public abstract void ApplyAction(TConstructable obj, ConstructReaderStream inputStream);
    }
}