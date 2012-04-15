using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class Int64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, long>  _assignmentFunction;
        private readonly Func<TConstructable, long> _readerFunction;

        public Int64Action(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, Int64>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Int64>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            long value = inputStream.ReadInt64(InputByteOrder);
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteInt64(value, InputByteOrder);
        }
    }
}