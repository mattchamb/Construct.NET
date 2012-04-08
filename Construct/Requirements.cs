using System;

namespace Construct
{
    internal static class Requirements
    {
        public static void RequireNotNull<T>(this T value, string argName)
        {
            if(argName == null)
                Fault.ArgumentNull("argName");
            if(value == null)
                Fault.ArgumentNull(argName);
        }

        public static void Require<T>(this T value, string argName, Func<T, bool> requirement)
        {
            requirement.RequireNotNull("requirement");
            argName.RequireNotNull("argName");
            value.RequireNotNull("value");

            if(!requirement(value))
            {
                Fault.ArgumentInvalid(argName);
            }
        }
    }
}
