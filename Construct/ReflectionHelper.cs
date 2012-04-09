using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Construct
{
    public static class ReflectionHelper
    {
        public static bool IsConstructable(this ICustomAttributeProvider type)
        {
            return type.GetCustomAttributes(typeof (ConstructableAttribute), false).Any();
        }

        public static IList<T> GetAttributes<T>(this ICustomAttributeProvider type)
        {
            return type.GetCustomAttributes(typeof(T), false).Cast<T>().ToList();
        }

        public static IList<T> GetAllAttributes<T>(this ICustomAttributeProvider type) where T : class
        {
            return type.GetCustomAttributes(false).OfType<T>().ToList();
        }

        public static PropertyConstructDescriptor GetElementDescriptor(this PropertyInfo propertyInfo)
        {
            var descriptors = propertyInfo.GetCustomAttributes(false).OfType<IConstructElementDescriptor>().ToList();
            if(descriptors.Count == 0)
            {
                return null;
            }
            if(descriptors.Count > 1)
            {
                throw new TooManyDescriptorsException(string.Format(CultureInfo.CurrentCulture,
                                                                    "The property \"{0}\" has multiple element descriptors. " +
                                                                    "Please check the definition of this property to remove the un-wanted descriptors.",
                                                                    propertyInfo.Name));
            }
            return new PropertyConstructDescriptor(propertyInfo, descriptors[0]);
        }
    }
}
