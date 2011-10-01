using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;
using Construct.NET.Interfaces;

namespace Construct.NET
{
    internal class ConstructPlanner : IConstructPlanner
    {
        private static object _syncRoot = new object();
        public static Dictionary<Type, ConstructPlan> CachedPlans = new Dictionary<Type, ConstructPlan>();

        private Dictionary<Type, List<Type>> _mappings;
        private Dictionary<Type, List<Type>> TypeActionMappings
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

            var constructPlan = new ConstructPlan();
            
            var constructProperties = type.GetTargetProperties();

            foreach (var property in constructProperties)
            {
                ConstructPlanAction action;
                if(property.Property.PropertyType.IsArray)
                {
                    //Gets the array length as the value of the property immediately before - need to make it more flexible.
                    action = new ArrayAction(constructProperties.First(x => x.SerializationOrder == property.SerializationOrder-1).Property, property.Property);
                }
                else if(property.Property.PropertyType.IsEnum)
                {
                    action = new EnumAction(property.Property);
                }
                else if(property.Property.PropertyType.IsConstruct())
                {
                    action = new NestedAction(property.Property);
                }
                else
                {
                    var actionType = TypeActionMappings[property.Property.PropertyType];
                    action = (ConstructPlanAction)Activator.CreateInstance(actionType.First(), property.Property);
                }
                constructPlan.PlanActions.Add(action);
            }
            CachedPlans.Add(type, constructPlan);
            return constructPlan;
        }

    }
}