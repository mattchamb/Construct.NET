using System;
using System.Reflection;

namespace Construct.Actions
{
    public class ComplexAction<TConstructable, TNestedConstructable> : PlanAction<TConstructable> where TNestedConstructable : new()
    {
        private readonly IConstructPlanner<TNestedConstructable> _constructPlanner;
        private readonly Lazy<Action<TConstructable, object>> _assignmentFunction;

        /// <param name="property"></param>
        /// <param name="inputByteOrder">
        ///     This doesnt matter for "Complex" types because they 
        ///     will have their own nested byte order directives.
        /// </param>
        /// <param name="constructPlanner"></param>
        public ComplexAction(PropertyInfo property, ByteOrder inputByteOrder, IConstructPlanner<TNestedConstructable> constructPlanner) 
            : base(property, inputByteOrder)
        {
            _constructPlanner = constructPlanner;
            _assignmentFunction = new Lazy<Action<TConstructable, object>>(() => LambdaGenerator.CreateAssignmentFunctionWithCast<TConstructable, object>(Property));
        }

        public override void ApplyAction(TConstructable obj, ConstructReaderStream inputStream)
        {
            var nestedConstructPlan = _constructPlanner.CreatePlan();
            var value = nestedConstructPlan.Parse(inputStream);
            var assignmentFunction = _assignmentFunction.Value;
            assignmentFunction(obj, value);
        }
    }
}