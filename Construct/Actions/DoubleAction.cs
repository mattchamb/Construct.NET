using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class DoubleAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, double> _assignmentFunction;
        private readonly Func<TConstructable, double> _readerFunction;

        public DoubleAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, double>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, double>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var value = inputStream.ReadDouble(InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteDouble(value, InputByteOrder);
        }
    }
}