using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Construct.NET
{
    internal static class ReflectionHelper
    {
        public static bool IsConstruct(this Type type)
        {
            return type.GetCustomAttributes(typeof (ConstructAttribute), true).Any();
        }

        //TODO: Adjust where to search for derived types so that it is easier to extend
        public static IEnumerable<Type> GetDerivedTypes(this Type type)
        {
            return type.Assembly.GetTypes().Where(x => type.Equals(x.BaseType)).ToArray();
        }

        public static Type GetTarget(this Type type)
        {
            var attributes =
                type.GetCustomAttributes(typeof (ConstructTargetAttribute), false).Cast<ConstructTargetAttribute>();
            if(!attributes.Any())
                throw new Exception(string.Format("{0} has no defined ConstructTarget attribute", type));
            
            return attributes.First().Target;
        }

        public static IOrderedEnumerable<OrderedProperty> GetTargetProperties(this Type target)
        {
            var result = new List<OrderedProperty>();

            foreach (var property in target.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof (ConstructFieldAttribute), false);
                if (!attributes.Any())
                    continue;
                var fieldInfo = attributes.First() as ConstructFieldAttribute;
                result.Add(new OrderedProperty(fieldInfo.SerializationOrder, property));
            }

            return result.OrderBy(x => x.SerializationOrder);
        }

        public static int GetSerializationOrder(this PropertyInfo propInfo)
        {
            var attributes =
                propInfo.GetCustomAttributes(typeof(ConstructFieldAttribute), false).Cast<ConstructFieldAttribute>();
            if (!attributes.Any())
                throw new Exception(string.Format("{0} has no defined ConstructTarget attribute", propInfo));

            return attributes.First().SerializationOrder;
        }

        public static Dictionary<Type, Type> GetTypeActionMappings()
        {
            var actionTypes = typeof(ConstructPlanAction).GetDerivedTypes();
            return actionTypes.ToDictionary(action => action.GetTarget());
        }
    }

    internal class OrderedProperty
    {
        public int SerializationOrder { get; private set; }
        public PropertyInfo Property { get; private set; }
        public OrderedProperty(int serializationOrder, PropertyInfo property)
        {
            SerializationOrder = serializationOrder;
            Property = property;
        }
    }
}
