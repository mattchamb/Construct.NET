using System;
using System.Reflection;

namespace Construct.Actions
{
    public class FloatAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, float> _assignmentFunction;
        private readonly Func<TConstructable, float> _readerFunction;

        public FloatAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, float>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, float>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var value = inputStream.ReadSingle(InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteSingle(value, InputByteOrder);
        }
    }
}