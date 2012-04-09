using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, long>>  _assignmentFunction;
        private readonly Lazy<Func<TConstructable, long>> _readerFunction;

        public Int64Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Int64>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, Int64>(Property));
            _readerFunction = new Lazy<Func<TConstructable, Int64>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, Int64>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            long value = inputStream.ReadInt64(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteInt64(value, InputByteOrder);
        }
    }
}