using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int32Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, Int32> _assignmentFunction;
        private readonly Func<TConstructable, Int32> _readerFunction;

        public Int32Action(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, Int32>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Int32>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            int value = inputStream.ReadInt32(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteInt32(value, InputByteOrder);
        }
    }
}