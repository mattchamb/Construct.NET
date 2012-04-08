using System;
using System.Reflection;

namespace Construct.Actions
{
    public class Int32Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, Int32>> _assignmentFunction;

        public Int32Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Int32>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, Int32>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            int value = inputStream.ReadInt32(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}