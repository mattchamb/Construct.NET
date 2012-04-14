using System;

namespace Construct
{
    internal static class Require
    {
        public static void NotNull<T>(T value, string argName)
        {
            if(argName == null)
            {
                throw new ArgumentNullException("argName");
            }
            if(value == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        public static void That<T>(T value, string argName, bool requirement)
        {
            NotNull(argName, "argName");
            NotNull(value, "value");

            if(!requirement)
            {
                Fault.ArgumentInvalid(argName);
            }
        }
    }
}
