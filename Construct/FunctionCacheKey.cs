using System;
using System.Reflection;

namespace Construct
{
    public class FunctionCacheKey : IEquatable<FunctionCacheKey>
    {
        public PropertyInfo PropertyInfo { get; private set; }
        public Type ArgumentType { get; private set; }
        public Type ConstructType { get; private set; }

        public FunctionCacheKey(Type constructType, Type argumentType, PropertyInfo propertyInfo)
        {
            ConstructType = constructType;
            ArgumentType = argumentType;
            PropertyInfo = propertyInfo;
        }

        public bool Equals(FunctionCacheKey other)
        {
            return ArgumentType == other.ArgumentType && ConstructType == other.ConstructType && PropertyInfo == other.PropertyInfo;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ArgumentType.GetHashCode() ^ ConstructType.GetHashCode() ^ PropertyInfo.GetHashCode();
            }
        }

    }
}