using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, short>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, short>> _readerFunction;

        public Int16Action(PropertyInfo property, ByteOrder inputByteOrder)
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Int16>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, Int16>(Property));
            _readerFunction = new Lazy<Func<TConstructable, Int16>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, Int16>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            short value = inputStream.ReadInt16(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteInt16(value, InputByteOrder);
        }
    }
}