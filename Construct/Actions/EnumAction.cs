using System;
using System.Reflection;

namespace Construct.Actions
{
    public class EnumAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Lazy<Action<TConstructable, Enum>>  _assignmentFunction;
        private readonly Lazy<Func<TConstructable, Enum>>  _readerFunction;

        public EnumAction(PropertyInfo property, ByteOrder inputByteOrder) 
            : base(property, inputByteOrder)
        {
            _assignmentFunction = new Lazy<Action<TConstructable, Enum>>(() => LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, Enum>(Property));
            _readerFunction = new Lazy<Func<TConstructable, Enum>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, Enum>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            Enum value = inputStream.ReadEnum(Property.PropertyType, InputByteOrder);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var reader = _readerFunction.Value;
            var value = reader(obj);
            outputStream.WriteEnum(value, InputByteOrder);
        }
    }
}