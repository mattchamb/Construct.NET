using System;
using System.IO;
using System.Reflection;

namespace Construct.NET
{
    internal abstract class ConstructPlanAction
    {
        public PropertyInfo TargetProperty { get; private set; }
        protected MethodInfo SetterMethod;
        protected MethodInfo GetterMethod;
        public abstract Type TargetType { get; }

        protected ConstructPlanAction(PropertyInfo targetProperty)
        {
            TargetProperty = targetProperty;
            SetterMethod = TargetProperty.GetSetMethod();
            GetterMethod = TargetProperty.GetGetMethod();
        }

        public abstract void Execute(BinaryReader reader, object targetObj);
        public abstract void Output(BinaryWriter writer, object targetObj);

        protected void CheckTypes(object targetObj)
        {
            if (!targetObj.GetType().Equals(TargetProperty.DeclaringType))
            {
                throw new ArgumentException(
                    string.Format("The property \"{0}\" does not belong to the type {1}",
                                  TargetProperty, targetObj.GetType()));
            }
            if (!TargetProperty.PropertyType.Equals(TargetType))
            {
                throw new Exception(
                    string.Format("PropertyType \"{0}\" does not match the TargetType \"{1}\"",
                                  TargetProperty, TargetType));
            }
        }

        protected internal abstract object GetValue(BinaryReader reader);
    }
}