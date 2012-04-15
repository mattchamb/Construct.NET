using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class UInt64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, ulong>  _assignmentFunction;
        private readonly Func<TConstructable, ulong> _readerFunction;

        public UInt64Action(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt64>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, UInt64>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            ulong value = inputStream.ReadUInt64(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteUInt64(value, InputByteOrder);
        }
    }
}