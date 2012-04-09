using System;
using System.Reflection;

namespace Construct.Actions
{
    public class ComplexAction<TConstructable, TNestedConstructable> : PlanAction<TConstructable> where TNestedConstructable : new()
    {
        private readonly IConstructPlanner _constructPlanner;
        private readonly Lazy<Action<TConstructable, object>> _assignmentFunction;
        private readonly Lazy<Func<TConstructable, TNestedConstructable>> _readerFunction;

        /// <param name="property"></param>
        /// <param name="inputByteOrder">This doesnt matter for "Complex" types because they will have their own nested byte order directives.</param>
        /// <param name="constructPlanner"></param>
        public ComplexAction(PropertyInfo property, ByteOrder inputByteOrder, IConstructPlanner constructPlanner) 
            : base(property, inputByteOrder)
        {
            _constructPlanner = constructPlanner;
            _assignmentFunction = new Lazy<Action<TConstructable, object>>(() => LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, object>(Property));
            _readerFunction = new Lazy<Func<TConstructable, TNestedConstructable>>(() => LambdaGenerator.CreateReaderFunction<TConstructable, TNestedConstructable>(Property));
        }

        public override void ApplyReadAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            var nestedConstructPlan = _constructPlanner.CreatePlan<TNestedConstructable>();
            var value = nestedConstructPlan.ReadConstruct(inputStream);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }

        public override void ApplyWriteAction(TConstructable obj, ConstructWriterStream outputStream)
        {
            var nestedConstructPlan = _constructPlanner.CreatePlan<TNestedConstructable>();
            var reader = _readerFunction.Value;
            var nestedConstructable = reader(obj);
            nestedConstructPlan.WriteConstruct(nestedConstructable, outputStream);
        }
    }
}