using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, ushort>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, ushort>> _readerFunction;

        public UInt16Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, UInt16>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt16>(Property));
            _readerFunction = new Lazy<Func<TConstructable, UInt16>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, UInt16>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            ushort value = inputStream.ReadUInt16(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteUInt16(value, InputByteOrder);
        }
    }
}