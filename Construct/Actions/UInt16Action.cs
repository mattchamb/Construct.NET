using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class UInt16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, ushort> _assignmentFunction;
        private readonly Func<TConstructable, ushort> _readerFunction;

        public UInt16Action(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt16>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, UInt16>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            ushort value = inputStream.ReadUInt16(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteUInt16(value, InputByteOrder);
        }
    }
}