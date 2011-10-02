using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Construct.NET.Interfaces;

namespace Construct.NET
{
    internal class ConstructPlanner : IConstructPlanner
    {
        private readonly static object SyncRoot = new object();
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
        /// Because a type can have multiple actions, e.g. strings, we need to
        /// resolve the intended type based on the fields set in the attribute.
        /// </summary>
        /// <param name="actionGroup"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private Type ResolveAction(IEnumerable<Type> actionGroup, ConstructProperty property)
        {
            if(property.Property.PropertyType == typeof(string))
            {
                return ChooseStringType(property);
            }
            //currently only strings have multiple handlers
            //  so these groups will only have one element.
            return actionGroup.First();
        }

        private Type ChooseStringType(ConstructProperty property)
        {
            if(property.StringLength > 0)
            {
                return typeof (FixedLengthStringAction);
            }
            return typeof (AsciiStringAction);
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