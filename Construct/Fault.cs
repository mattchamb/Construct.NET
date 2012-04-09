using System;

namespace Construct
{
    public static class Fault
    {
        public static void ArgumentNull(string argName)
        {
            throw new ArgumentNullException(argName);
        }

        public static void ArgumentInvalid(string argName)
        {
            throw new ArgumentException("The provided argument did not meet the specified requirements.", argName);
        }

        public static void ArgumentInvalid(string message, string argName)
        {
            throw new ArgumentException(message, argName);
        }

        public static void NotEnoughData(string message)
        {
            throw new NotEnoughDataException(message);
        }
    }
}
