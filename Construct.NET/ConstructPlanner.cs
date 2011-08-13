using System;
using System.Collections.Generic;
using System.Linq;
using Construct.NET.Attributes;
using System.Reflection;

namespace Construct.NET
{
    internal class ConstructPlanner : IConstructPlanner
    {
        private static object _syncRoot = new object();
        public static Dictionary<Type, ConstructPlan> CachedPlans = new Dictionary<Type, ConstructPlan>();

        private Dictionary<Type, Type> _mappings;
        private Dictionary<Type, Type> TypeActionMappings
        {
            get
            {
                return _mappings ?? (_mappings = GetTypeActionMappings());
            }
        }

        public ConstructPlan CreateConstructPlan<T>()
        {
            return CreateConstructPlan(typeof (T));
        }

        public ConstructPlan CreateConstructPlan(Type type)
        {
            if(CachedPlans.ContainsKey(type))
            {
                return CachedPlans[type];
            }

            var result = new ConstructPlan
                             {
                                 PlanActions = new List<ConstructPlanAction>()
                             };
            if(!type.IsConstruct())
            {
                throw new ArgumentException(string.Format("The specified type ({0}) is not a Construct", type), "type");
            }

            var constructProperties = type.GetTargetProperties();

            foreach (var property in constructProperties)
            {
                if(property.Item2.PropertyType.IsConstruct())
                {
                    result.PlanActions.Add(new NestedAction(property.Item2));
                }
                else
                {
                    var actionType = TypeActionMappings[property.Item2.PropertyType];
                    result.PlanActions.Add((ConstructPlanAction)Activator.CreateInstance(actionType, property.Item2));
                }
            }

            CachedPlans.Add(type, result);
            return result;
        }

        private Dictionary<Type, Type> GetTypeActionMappings()
        {
            var actionTypes = typeof (ConstructPlanAction).GetDerivedTypes();
            return actionTypes.ToDictionary(action => action.GetTarget());
        }
    }
}