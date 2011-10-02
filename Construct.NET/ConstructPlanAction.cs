using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Construct.NET
{
    internal abstract class ConstructPlanAction
    {
        public ConstructProperty TargetProperty { get; private set; }
        protected MethodInfo SetterMethod;
        protected MethodInfo GetterMethod;
        
        public string ConditionalProperty { get; private set; }
        public Func<object, bool> ConditionalFunction { get; private set; }

        protected ConstructPlanAction(ConstructProperty targetProperty)
        {
            TargetProperty = targetProperty;
            SetterMethod = TargetProperty.Property.GetSetMethod();
            GetterMethod = TargetProperty.Property.GetGetMethod();

            ConditionalProperty = TargetProperty.ConditionArgument;
            ConditionalFunction = TargetProperty.Condition;
        }

        private Type _targetType;
        public Type TargetType
        {
            get
            {
                return _targetType ?? (_targetType = GetType().GetTarget());
            }
        }

        public abstract void Execute(BinaryReader reader, object targetObj);
        public abstract void Output(BinaryWriter writer, object targetObj);
        protected internal abstract object GetValue(BinaryReader reader);

        protected void CheckTypes(object targetObj)
        {
            if (!targetObj.GetType().Equals(TargetProperty.Property.DeclaringType))
            {
                throw new ArgumentException(
                    string.Format("The property \"{0}\" does not belong to the type {1}",
                                  TargetProperty, targetObj.GetType()));
            }
            if (!TargetProperty.Property.PropertyType.Equals(TargetType))
            {
                throw new Exception(
                    string.Format("PropertyType \"{0}\" does not match the TargetType \"{1}\"",
                                  TargetProperty, TargetType));
            }
        }

        public bool IsConditionalAction
        {
            get
            {
                return ConditionalProperty != null && ConditionalFunction != null;
            }
        }
    }
}