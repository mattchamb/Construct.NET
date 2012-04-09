using System;
using System.Reflection;

namespace Construct.Actions
{
    public class BooleanAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, bool>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, bool>> _readerFunction;

        public BooleanAction(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, bool>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, bool>(Property));
            _readerFunction = new Lazy<Func<TConstructable, bool>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, bool>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            bool value = inputStream.ReadBoolean();
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteBoolean(value);
        }
    }
}