using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt32Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, uint>>  _assignmentFunction;

        public UInt32Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, uint>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt32>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            uint value = inputStream.ReadUInt32(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}