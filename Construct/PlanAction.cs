using System.Reflection;

namespace Construct
{
    public abstract class PlanAction<TConstructable>
    {
        protected ILambdaGenerator LambdaGenerator { get; private set; }
        public PropertyInfo Property { get; private set; }
        public ByteOrder InputByteOrder { get; private set; }

        protected PlanAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
        {
            Require.NotNull(property, "property");
            Require.NotNull(lambdaGenerator, "lambdaGenerator");
            LambdaGenerator = lambdaGenerator;
            Property = property;
            InputByteOrder = inputByteOrder;
        }

        public abstract void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner);

        public abstract void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner);
    }
}