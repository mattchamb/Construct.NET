using System;
using System.Reflection;

namespace Construct.Actions
{
    public class ComplexAction<TConstructable, TNestedConstructable> : PlanAction<TConstructable> where TNestedConstructable : new()
    {
        private readonly Action<TConstructable, object> _assignmentFunction;
        private readonly Func<TConstructable, TNestedConstructable> _readerFunction;

        /// <param name="property"></param>
        /// <param name="inputByteOrder">This doesnt matter for "Complex" types because they will have their own nested byte order directives.</param>
        /// <param name="lambdaGenerator"></param>
        public ComplexAction(PropertyInfo property, ByteOrder inputByteOrder, ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, lambdaGenerator)
        {
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, object>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, TNestedConstructable>(Property);
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var nestedConstructPlan = constructPlanner.CreatePlan<TNestedConstructable>();
            var value = nestedConstructPlan.ReadConstruct(inputStream);
            _assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var nestedConstructPlan = constructPlanner.CreatePlan<TNestedConstructable>();
            var nestedConstructable = _readerFunction(obj);
            nestedConstructPlan.WriteConstruct(nestedConstructable, outputStream);
        }
    }
}