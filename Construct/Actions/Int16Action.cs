using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, short> _assignmentFunction;
        private readonly Func<TConstructable, short> _readerFunction;

        public Int16Action(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, Int16>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Int16>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            short value = inputStream.ReadInt16(InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteInt16(value, InputByteOrder);
        }
    }
}