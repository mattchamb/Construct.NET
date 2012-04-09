using System;
using System.Collections.Generic;
using System.Linq;

namespace Construct
{
    public class ConstructPlanner : IConstructPlanner
    {
        private readonly ITypeActionResolver _actionResolver;

        public ConstructPlanner(ITypeActionResolver actionResolver)
        {
            _actionResolver = actionResolver;
        }

        public ConstructPlan<TConstructable> CreatePlan<TConstructable>() where TConstructable : new()
        {
            var planActions = CreatePlanActionsForType<TConstructable>();
            return new ConstructPlan<TConstructable>(planActions, this);
        }

        private List<PlanAction<TConstructable>> CreatePlanActionsForType<TConstructable>() where TConstructable : new()
        {
            var constructType = typeof (TConstructable);
            constructType.Require("TConstructable", t => t.IsConstructable());

            var constructProperties = constructType.GetProperties();
            var propertyDescriptors = constructProperties.Select(property => property.GetElementDescriptor()).Where(x => x != null);

            var orderedDescriptors = propertyDescriptors.OrderBy(x => x.Descriptor.SerializationOrder);

            var actions = orderedDescriptors.Select(x => _actionResolver.ResolveActionForType<TConstructable>(x));

            return new List<PlanAction<TConstructable>>(actions);
        }
    }
}