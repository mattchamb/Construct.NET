using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class ByteAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, byte> _assignmentFunction;
        private readonly Func<TConstructable, byte> _readerFunction;

        public ByteAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, byte>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, byte>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            byte value = inputStream.ReadByte();
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteByte(value);
        }
    }
}