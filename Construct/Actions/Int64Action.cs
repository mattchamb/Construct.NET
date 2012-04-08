using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, long>>  _assignmentFunction;

        public Int64Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Int64>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, Int64>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            long value = inputStream.ReadInt64(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}