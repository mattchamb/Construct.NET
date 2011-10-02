using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Construct.NET
{
    public class ConstructPlanner : IConstructPlanner
    {
        private readonly static object SyncRoot = new object();
        internal static Dictionary<Type, ConstructPlan> CachedPlans = new Dictionary<Type, ConstructPlan>();

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

            foreach (var constructProperty in constructProperties)
            {
                ConstructPlanAction action;
                if(constructProperty.IsArray)
                {
                    //Gets the array length as the value of the property immediately before - need to make it more flexible.
                    action =
                        new ArrayAction(
                            constructProperties.First(
                                x => x.SerializationOrder == constructProperty.SerializationOrder - 1).Property,
                            constructProperty);
                }
                else if(constructProperty.Property.PropertyType.IsEnum)
                {
                    action = new EnumAction(constructProperty);
                }
                else if(constructProperty.IsConstruct)
                {
                    action = new NestedAction(constructProperty);
                }
                else
                {
                    var actionGroup = TypeActionMappings[constructProperty.Property.PropertyType];
                    var actionType = ResolveAction(actionGroup, constructProperty);
                    action = CreatePlanAction(actionType, constructProperty);
                }
                constructPlan.PlanActions.Add(action);
            }
            CachedPlans.Add(type, constructPlan);
            return constructPlan;
        }

        /// <summary>
        /// Because I think that a type might have multiple actions we need to
        /// resolve the intended type based on the fields set in the attribute.
        /// </summary>
        /// <param name="actionGroup"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private Type ResolveAction(List<Type> actionGroup, ConstructProperty property)
        {
            //Currently no multiple actions, so just return the first one.
            Debug.Assert(actionGroup.Count == 1, "actionGroup.Count == 1",
                         "A type has multiple actions. This function needs to be extended to handle that.");
            return actionGroup[0];
        }

        private ConstructPlanAction CreatePlanAction(Type actionType, ConstructProperty targetProperty)
        {
            var constructorInfo = actionType.GetConstructor(BindingFlags.Instance | BindingFlags.Public,
                                                            null,
                                                            CallingConventions.HasThis,
                                                            new[] {typeof (ConstructProperty)},
                                                            null);
            var constructorArg = Expression.Constant(targetProperty);
            var constructorExpression = Expression.New(constructorInfo, constructorArg);
            var ctorFunction = Expression.Lambda<Func<ConstructPlanAction>>(constructorExpression).Compile();
            return ctorFunction();
        }

    }
}