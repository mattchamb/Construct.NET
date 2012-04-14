using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, long>  _assignmentFunction;
        private readonly Func<TConstructable, long> _readerFunction;

        public Int64Action(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, Int64>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Int64>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            long value = inputStream.ReadInt64(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteInt64(value, InputByteOrder);
        }
    }
}