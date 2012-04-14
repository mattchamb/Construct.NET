using System;
using System.Collections.Generic;
using System.Linq;

namespace Construct
{
    public class ConstructPlanner : IConstructPlanner
    {
        private readonly ITypeActionResolver _actionResolver;
        private readonly Dictionary<Type, object> _planCache;

        public ConstructPlanner() 
            : this(new DefaultTypeActionResolver(new CachedLambdaGenerator()))
        {
        }

        public ConstructPlanner(ITypeActionResolver actionResolver)
        {
            _actionResolver = actionResolver;
            _planCache = new Dictionary<Type, object>();
        }

        public ConstructPlan<TConstructable> CreatePlan<TConstructable>() where TConstructable : new()
        {
            object cachedPlan;
            if(_planCache.TryGetValue(typeof(TConstructable), out cachedPlan))
            {
                return cachedPlan as ConstructPlan<TConstructable>;
            }

            var planActions = CreatePlanActionsForType<TConstructable>();
            var plan = new ConstructPlan<TConstructable>(planActions, this);
            _planCache.Add(typeof(TConstructable), plan);
            return plan;
        }

        private List<PlanAction<TConstructable>> CreatePlanActionsForType<TConstructable>() where TConstructable : new()
        {
            var constructType = typeof (TConstructable);
            Require.That(constructType, "TConstructable", constructType.IsConstructable());

            var constructProperties = constructType.GetProperties();
            var propertyDescriptors = constructProperties.Select(property => property.GetElementDescriptor()).Where(x => x != null);

            var orderedDescriptors = propertyDescriptors.OrderBy(x => x.Descriptor.SerializationOrder);

            var actions = orderedDescriptors.Select(x => _actionResolver.ResolveActionForType<TConstructable>(x));

            return new List<PlanAction<TConstructable>>(actions);
        }
    }
}