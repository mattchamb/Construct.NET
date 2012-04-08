using System;
using System.Reflection;

namespace Construct.Actions
{
    public class UInt16Action<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, ushort>>  _assignmentFunction;

        public UInt16Action(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, UInt16>>(() => LambdaGenerator.CreateAssignmentFunction<TConstructable, UInt16>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            ushort value = inputStream.ReadUInt16(InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}