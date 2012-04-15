using System;
using System.Reflection;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class ComplexAction<TConstructable, TNestedConstructable> : PlanAction<TConstructable> where TNestedConstructable : new()
    {
        private readonly Action<TConstructable, object> _assignmentFunction;
        private readonly Func<TConstructable, TNestedConstructable> _readerFunction;

        /// <param name="property"></param>
        /// <param name="inputByteOrder">This doesnt matter for "Complex" types because they will have their own nested byte order directives.</param>
        /// <param name="lambdaGenerator"></param>
        public ComplexAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, object>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, TNestedConstructable>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var nestedConstructPlan = constructPlanner.CreatePlan<TNestedConstructable>();
            var value = nestedConstructPlan.ReadConstruct(inputStream);
            _assignmentFunction(obj, value);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var nestedConstructPlan = constructPlanner.CreatePlan<TNestedConstructable>();
            var nestedConstructable = _readerFunction(obj);
            nestedConstructPlan.WriteConstruct(nestedConstructable, outputStream);
        }
    }
}