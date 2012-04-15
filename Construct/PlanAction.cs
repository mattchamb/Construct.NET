using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct
{
    public abstract class PlanAction<TConstructable>
    {
        protected ILambdaGenerator LambdaGenerator { get; private set; }
        public PropertyInfo Property { get; private set; }
        public ByteOrder InputByteOrder { get; private set; }
        private readonly Func<TConstructable, bool> _conditionFunction;

        protected PlanAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
        {
            Require.NotNull(property, "property");
            Require.NotNull(lambdaGenerator, "lambdaGenerator");
            LambdaGenerator = lambdaGenerator;
            Property = property;
            InputByteOrder = inputByteOrder;
            if (!string.IsNullOrEmpty(conditionFunction))
            {
                _conditionFunction = LambdaGenerator.CreateConditionCheckingFunction<TConstructable>(conditionFunction);
            }
        }

        public void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            Require.NotNull(obj, "obj");
            Require.NotNull(inputStream, "inputStream");
            Require.NotNull(constructPlanner, "constructPlanner");
            if (_conditionFunction != null && !_conditionFunction(obj))
                return;
            Read(obj, inputStream, constructPlanner);
        }

        public void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            Require.NotNull(obj, "obj");
            Require.NotNull(outputStream, "outputStream");
            Require.NotNull(constructPlanner, "constructPlanner");
            if (_conditionFunction != null && !_conditionFunction(obj))
                return;
            Write(obj, outputStream, constructPlanner);
        }
        protected abstract void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner);
        protected abstract void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner);
    }
}