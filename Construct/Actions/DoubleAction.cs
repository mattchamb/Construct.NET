using System;
using System.Reflection;

namespace Construct.Actions
{
    public class DoubleAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, double> _assignmentFunction;
        private readonly Func<TConstructable, double> _readerFunction;

        public DoubleAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, double>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, double>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var value = inputStream.ReadDouble(InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteDouble(value, InputByteOrder);
        }
    }
}