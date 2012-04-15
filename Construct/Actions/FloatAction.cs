using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class FloatAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, float> _assignmentFunction;
        private readonly Func<TConstructable, float> _readerFunction;

        public FloatAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, float>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, float>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var value = inputStream.ReadSingle(InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteSingle(value, InputByteOrder);
        }
    }
}