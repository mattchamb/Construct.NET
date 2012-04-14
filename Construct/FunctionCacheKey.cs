using System;
using System.Reflection;

namespace Construct
{
    public class FunctionCacheKey : IEquatable<FunctionCacheKey>
    {
        private readonly Type _tConstruct;
        private readonly Type _tArg;
        private readonly PropertyInfo _property;

        public FunctionCacheKey(Type tConstruct, Type tArg, PropertyInfo property)
        {
            _tConstruct = tConstruct;
            _tArg = tArg;
            _property = property;
        }

        public PropertyInfo Property
        {
            get { return _property; }
        }

        public Type Arg
        {
            get { return _tArg; }
        }

        public Type Construct
        {
            get { return _tConstruct; }
        }

        public bool Equals(FunctionCacheKey other)
        {
            return Arg == other.Arg && Construct == other.Construct && Property == other.Property;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Arg.GetHashCode() ^ Construct.GetHashCode() ^ Property.GetHashCode();
            }
        }

    }
}