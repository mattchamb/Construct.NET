using System;
using System.Reflection;

namespace Construct.Actions
{
    public class BooleanAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, bool> _assignmentFunction;
        private readonly Func<TConstructable, bool> _readerFunction;

        public BooleanAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator) 
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, bool>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, bool>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            bool value = inputStream.ReadBoolean();
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var value = _readerFunction(obj);
            outputStream.WriteBoolean(value);
        }
    }
}