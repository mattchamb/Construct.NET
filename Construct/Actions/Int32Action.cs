using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int32Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, Int32>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, Int32>> _readerFunction;

        public Int32Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Int32>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, Int32>(Property));
            _readerFunction = new Lazy<Func<TConstructable, Int32>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, Int32>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            int value = inputStream.ReadInt32(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteInt32(value, InputByteOrder);
        }
    }
}