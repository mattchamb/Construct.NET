using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace Construct.NET
{
    public static class ReflectionHelper
    {
        public static bool IsConstruct(this Type type)
        {
            return type.GetCustomAttributes(typeof (ConstructAttribute), false).Any();
        }

        //TODO: Adjust where to search for derived types so that it is easier to extend
        public static IEnumerable<Type> GetDerivedTypes(this Type type)
        {
            return type.Assembly.GetTypes().Where(x => type.Equals(x.BaseType)).ToArray();
        }

        public static IOrderedEnumerable<ConstructProperty> GetTargetProperties(this Type target)
        {
            var result = new List<ConstructProperty>();

            foreach (var property in target.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof (ConstructFieldAttribute), false);
                if (!attributes.Any())
                    continue;
                result.Add(new ConstructProperty(property));
            }

            return result.OrderBy(x => x.SerializationOrder);
        }

        public static ConstructFieldAttribute GetConstructFieldAttribute(this PropertyInfo propInfo)
        {
            var attributes =
                propInfo.GetCustomAttributes(typeof(ConstructFieldAttribute), false).Cast<ConstructFieldAttribute>();
            if (!attributes.Any())
                throw new Exception(string.Format("{0} has no defined ConstructTarget attribute", propInfo));

            return attributes.First();
        }

        public static Type GetTarget(this Type type)
        {
            var attributes =
                type.GetCustomAttributes(typeof(ConstructTargetAttribute), false).Cast<ConstructTargetAttribute>();
            if (!attributes.Any())
                throw new Exception(string.Format("{0} has no defined ConstructTarget attribute", type));

            return attributes.First().Target;
        }

        public static Dictionary<Type, List<Type>> GetTypeActionMappings()
        {
            var actionTypes = typeof(ConstructPlanAction).GetDerivedTypes();
            var actionGroups = actionTypes.GroupBy(action => action.GetTarget());
            return actionGroups.ToDictionary(actionGroup => actionGroup.Key, actionGroup => actionGroup.ToList());
        }

        public static object GetFieldByName(this object obj, string fieldName)
        {
            var accessor = Expression.Lambda<Func<object>>(Expression.PropertyOrField(Expression.Constant(obj), fieldName)).Compile();
            return accessor();
        }
    }
}
