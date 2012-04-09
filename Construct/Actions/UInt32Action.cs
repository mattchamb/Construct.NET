using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt32Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, uint>>  _assignmentFunction;
        private readonly Lazy<Func<TConstructable, uint>> _readerFunction;

        public UInt32Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, uint>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt32>(Property));
            _readerFunction = new Lazy<Func<TConstructable, uint>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, UInt32>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            uint value = inputStream.ReadUInt32(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteUInt32(value, InputByteOrder);
        }
    }
}