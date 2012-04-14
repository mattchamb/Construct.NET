using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, ushort> _assignmentFunction;
        private readonly Func<TConstructable, ushort> _readerFunction;

        public UInt16Action(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt16>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, UInt16>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            ushort value = inputStream.ReadUInt16(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteUInt16(value, InputByteOrder);
        }
    }
}