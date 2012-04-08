using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt64Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, ulong>>  _assignmentFunction;

        public UInt64Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, UInt64>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt64>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            ulong value = inputStream.ReadUInt64(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}