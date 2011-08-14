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
                return _mappings ?? (_mappings = ReflectionHelper.GetTypeActionMappings());
            }
        }

        public ConstructPlan CreateConstructPlan<T>()
        {
            return CreateConstructPlan(typeof (T));
        }

        public ConstructPlan CreateConstructPlan(Type type)
        {
            if (!type.IsConstruct())
            {
                throw new ArgumentException(string.Format("The specified type ({0}) is not a Construct", type), "type");
            }

            if(CachedPlans.ContainsKey(type))
            {
                return CachedPlans[type];
            }

            var result = new ConstructPlan
                             {
                                 PlanActions = new List<ConstructPlanAction>()
                             };
            
            var constructProperties = type.GetTargetProperties();

            foreach (var property in constructProperties)
            {
                if(property.Item2.PropertyType.IsArray)
                {
                    //Gets the array length as the value of the property immediately before - need to make it more flexible.
                    result.PlanActions.Add(new ArrayAction(constructProperties.First(x => x.Item1 == property.Item1-1).Item2, property.Item2));
                }
                else if(property.Item2.PropertyType.IsEnum)
                {
                    result.PlanActions.Add(new EnumAction(property.Item2));
                }
                else if(property.Item2.PropertyType.IsConstruct())
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

    }
}