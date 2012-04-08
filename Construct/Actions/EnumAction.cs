using System;
using System.Reflection;

namespace Construct.Actions
{
    public class EnumAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, Enum>>  _assignmentFunction;

        public EnumAction(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Enum>>(() => LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, Enum>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            Enum value = inputStream.ReadEnum(Property.PropertyType, InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}