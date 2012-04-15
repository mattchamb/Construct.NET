using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class Int16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, short> _assignmentFunction;
        private readonly Func<TConstructable, short> _readerFunction;

        public Int16Action(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, Int16>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Int16>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            short value = inputStream.ReadInt16(InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteInt16(value, InputByteOrder);
        }
    }
}