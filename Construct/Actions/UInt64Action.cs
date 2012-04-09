using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, ulong>>  _assignmentFunction;
        private readonly Lazy<Func<TConstructable, ulong>> _readerFunction;

        public UInt64Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, UInt64>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt64>(Property));
            _readerFunction = new Lazy<Func<TConstructable, UInt64>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, UInt64>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            ulong value = inputStream.ReadUInt64(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteUInt64(value, InputByteOrder);
        }
    }
}