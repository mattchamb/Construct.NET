using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class BooleanAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, bool> _assignmentFunction;
        private readonly Func<TConstructable, bool> _readerFunction;

        public BooleanAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator) 
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, bool>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, bool>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            bool value = inputStream.ReadBoolean();
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteBoolean(value);
        }
    }
}