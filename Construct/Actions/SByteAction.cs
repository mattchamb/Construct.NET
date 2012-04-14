using System;
using System.Reflection;

namespace Construct.Actions
{
    public class SByteAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, sbyte> _assignmentFunction;
        private readonly Func<TConstructable, sbyte> _readerFunction;

        public SByteAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, sbyte>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, sbyte>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            sbyte value = inputStream.ReadSByte();
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteSByte(value);
        }
    }
}