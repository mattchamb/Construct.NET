using System;
using System.Reflection;

namespace Construct.Actions
{
    public class FloatAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, float>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, float>> _readerFunction;

        public FloatAction(PropertyInfo property, ByteOrder inputByteOrder)
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, float>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, float>(Property));
            _readerFunction = new Lazy<Func<TConstructable, float>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, float>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            var value = inputStream.ReadSingle(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteSingle(value, InputByteOrder);
        }
    }
}