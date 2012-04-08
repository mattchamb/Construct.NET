using System;
using System.Reflection;

namespace Construct.Actions
{
    public class BooleanAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, bool>>  _assignmentFunction;

        public BooleanAction(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, bool>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, bool>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            bool value = inputStream.ReadBoolean();
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}