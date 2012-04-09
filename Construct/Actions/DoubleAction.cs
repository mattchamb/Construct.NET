using System;
using System.Reflection;

namespace Construct.Actions
{
    public class DoubleAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, double>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, double>> _readerFunction;

        public DoubleAction(PropertyInfo property, ByteOrder inputByteOrder)
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, double>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, double>(Property));
            _readerFunction = new Lazy<Func<TConstructable, double>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, double>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            var value = inputStream.ReadDouble(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteDouble(value, InputByteOrder);
        }
    }
}