using System.Reflection;

namespace Construct
{
    public abstract class PlanAction<TConstructable>
    {
        protected readonly ILambdaGenerator LambdaGenerator;
        public PropertyInfo Property { get; private set; }
        public ByteOrder InputByteOrder { get; private set; }

        protected PlanAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
        {
            LambdaGenerator = lambdaGenerator;
            property.RequireNotNull("property");
            Property = property;
            InputByteOrder = inputByteOrder;
        }

        public abstract void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner);

        public abstract void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner);
    }
}