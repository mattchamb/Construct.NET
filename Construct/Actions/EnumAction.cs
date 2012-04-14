using System;
using System.Reflection;

namespace Construct.Actions
{
    public class EnumAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, Enum>  _assignmentFunction;
        private readonly Func<TConstructable, Enum>  _readerFunction;

        public EnumAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, Enum>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Enum>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            Enum value = inputStream.ReadEnum(Property.PropertyType, InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteEnum(value, InputByteOrder);
        }
    }
}