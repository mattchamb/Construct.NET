using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(Enum))]
    public class EnumAction : ConstructPlanAction
    {
        public EnumAction(ConstructProperty targetProperty)
            : base(targetProperty)
        {
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            //CheckTypes(targetObj);
            var enumBaseType = TargetProperty.Property.PropertyType.GetEnumUnderlyingType();
            var actions = ReflectionHelper.GetTypeActionMappings();
            if(!actions.ContainsKey(enumBaseType))
            {
                throw new Exception(
                    string.Format("The enum {0} of base type {1} does not have a handler mapped for the base type.",
                                  TargetProperty.Property.PropertyType, enumBaseType));
            }
            var action = actions[enumBaseType];
            var planAction = (ConstructPlanAction)Activator.CreateInstance(action.First(), new object[] { TargetProperty }); // we don't need a TargetProperty.... hmmmm
            var result = planAction.GetValue(reader);

            bool isFlags = TargetProperty.Property.PropertyType.GetCustomAttributes(typeof(FlagsAttribute), false).Any();

            if (!isFlags && !TargetProperty.Property.PropertyType.IsEnumDefined(result))
            {
                throw new Exception(string.Format("The enum {0} is not defined for the value {1}",
                                                  TargetProperty.Property.PropertyType, result));
            }
            SetterMethod.Invoke(targetObj, new[] { result });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            throw new NotImplementedException();
        }

        internal override object GetValue(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}