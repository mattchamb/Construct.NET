using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Construct.NET.Attributes;

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
            var attributes = type.GetCustomAttributes(typeof (ConstructTargetAttribute), false).Cast<ConstructTargetAttribute>();
            if(!attributes.Any())
                throw new Exception(string.Format("{0} has no defined ConstructTarget attribute", type));
            
            return attributes.First().Target;
        }
    }
}
