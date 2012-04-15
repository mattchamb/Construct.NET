using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class EnumAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly Action<TConstructable, Enum>  _assignmentFunction;
        private readonly Func<TConstructable, Enum>  _readerFunction;

        public EnumAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, Enum>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, Enum>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            Enum value = inputStream.ReadEnum(Property.PropertyType, InputByteOrder);
            
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            
            var value = _readerFunction(obj);
            outputStream.WriteEnum(value, InputByteOrder);
        }
    }
}