using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace Construct.NET
{
    public abstract class ConstructPlanAction
    {
        public ConstructProperty TargetProperty { get; private set; }
        protected MethodInfo SetterMethod;
        protected MethodInfo GetterMethod;
        
        public MethodInfo ConditionalFunction { get; private set; }

        protected ConstructPlanAction(ConstructProperty targetProperty)
        {
            TargetProperty = targetProperty;
            SetterMethod = TargetProperty.Property.GetSetMethod();
            GetterMethod = TargetProperty.Property.GetGetMethod();
            if (!string.IsNullOrWhiteSpace(targetProperty.ConditionFunctionName))
            {
                ConditionalFunction = GetFunctionByName(targetProperty.Property.DeclaringType, TargetProperty.ConditionFunctionName);
            }
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
        internal abstract object GetValue(BinaryReader reader);

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
                return ConditionalFunction != null;
            }
        }

        private MethodInfo GetFunctionByName(Type declaringType, string functionName)
        {
            const BindingFlags bindingFlags = BindingFlags.InvokeMethod;
            var result =  declaringType.GetMethod(functionName);
            if (result.ReturnType != typeof(bool) || result.GetParameters().Length > 0)
                throw new Exception("The function specified by the conditional property must take no arguments and return a bool.");
            return result;
        }

        public bool InvokeConditionalFunction(object obj)
        {
            return (bool)ConditionalFunction.Invoke(obj, null);
        }
    }
}