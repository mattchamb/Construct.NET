using System;
using System.Reflection;

namespace Construct.Actions
{
    public class ByteAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, byte> _assignmentFunction;
        private readonly Func<TConstructable, byte> _readerFunction;

        public ByteAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, byte>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, byte>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            byte value = inputStream.ReadByte();
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteByte(value);
        }
    }
}