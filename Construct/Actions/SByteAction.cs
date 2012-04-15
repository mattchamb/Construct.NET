using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class SByteAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, sbyte> _assignmentFunction;
        private readonly Func<TConstructable, sbyte> _readerFunction;

        public SByteAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, sbyte>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, sbyte>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            sbyte value = inputStream.ReadSByte();
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteSByte(value);
        }
    }
}