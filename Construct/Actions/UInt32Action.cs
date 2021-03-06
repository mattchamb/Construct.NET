using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class UInt32Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, uint>  _assignmentFunction;
        private readonly Func<TConstructable, uint> _readerFunction;

        public UInt32Action(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt32>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, UInt32>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            uint value = inputStream.ReadUInt32(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction (obj);
            outputStream.WriteUInt32(value, InputByteOrder);
        }
    }
}