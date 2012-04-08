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
    }
}
