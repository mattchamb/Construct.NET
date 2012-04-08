using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, short>>  _assignmentFunction;

        public Int16Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Int16>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, Int16>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            short value = inputStream.ReadInt16(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}