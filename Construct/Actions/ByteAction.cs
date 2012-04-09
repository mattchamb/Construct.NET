using System;
using System.Reflection;

namespace Construct.Actions
{
    public class ByteAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, byte>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, byte>> _readerFunction;

        public ByteAction(PropertyInfo property, ByteOrder inputByteOrder)
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, byte>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, byte>(Property));
            _readerFunction = new Lazy<Func<TConstructable, byte>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, byte>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            byte value = inputStream.ReadByte();
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteByte(value);
        }
    }
}