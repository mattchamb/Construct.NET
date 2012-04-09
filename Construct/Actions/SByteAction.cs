using System;
using System.Reflection;

namespace Construct.Actions
{
    public class SByteAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, sbyte>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, sbyte>> _readerFunction;

        public SByteAction(PropertyInfo property, ByteOrder inputByteOrder)
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, sbyte>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, sbyte>(Property));
            _readerFunction = new Lazy<Func<TConstructable, sbyte>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, sbyte>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            sbyte value = inputStream.ReadSByte();
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteSByte(value);
        }
    }
}