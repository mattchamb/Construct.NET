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

        public abstract void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream);

        public abstract void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream);
    }
}