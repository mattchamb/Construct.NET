using System;

namespace Construct.Infrastructure
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

        public static void That(string argName, bool requirement)
        {
            NotNull(argName, "argName");
            if(!requirement)
            {
                throw new ArgumentException("The provided argument did not meet the specified requirements.", argName);
            }
        }

        public static void NotNullOrEmpty(string value, string argName)
        {
            NotNull(argName, "argName");
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("This value cannot be null or empty.", argName);
            }
        }
    }
}
