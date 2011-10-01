using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(Enum))]
    internal class EnumAction : ConstructPlanAction
    {
        public EnumAction(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(Enum); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            //CheckTypes(targetObj);
            var enumBaseType = TargetProperty.PropertyType.GetEnumUnderlyingType();
            var actions = ReflectionHelper.GetTypeActionMappings();
            if(!actions.ContainsKey(enumBaseType))
            {
                throw new Exception(
                    string.Format("The enum {0} of base type {1} does not have a handler mapped for the base type.",
                                  TargetProperty.PropertyType, enumBaseType));
            }
            var action = actions[enumBaseType];
            var planAction = (ConstructPlanAction)Activator.CreateInstance(action.First(), new object[] { TargetProperty }); // we don't need a TargetProperty.... hmmmm
            var result = planAction.GetValue(reader);

            bool isFlags = TargetProperty.PropertyType.GetCustomAttributes(typeof(FlagsAttribute), false).Any();

            if (!isFlags && !TargetProperty.PropertyType.IsEnumDefined(result))
            {
                throw new Exception(string.Format("The enum {0} is not defined for the value {1}",
                                                  TargetProperty.PropertyType, result));
            }
            SetterMethod.Invoke(targetObj, new[] { result });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            throw new NotImplementedException();
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}