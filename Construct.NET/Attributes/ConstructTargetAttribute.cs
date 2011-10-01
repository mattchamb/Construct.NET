using System;

namespace Construct.NET
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConstructTargetAttribute : Attribute
    {
        public Type Target { get; private set; }
        public ConstructTargetAttribute(Type target)
        {
            if(target == null)
                throw new ArgumentNullException("target");
            Target = target;
        }
    }
}