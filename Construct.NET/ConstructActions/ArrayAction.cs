using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(Array))]
    public class ArrayAction : ConstructPlanAction
    {
        public ArrayAction(PropertyInfo lengthProperty, ConstructProperty targetProperty)
            : base(targetProperty)
        {
            var lengthAttribute = lengthProperty.GetConstructFieldAttribute();
            if (!(targetProperty.SerializationOrder > lengthAttribute.SerializationOrder))
            {
                throw new Exception("Cannot deserialize an array without knowing its length - it must use a property assigned before the array.");
            }
            _arrayLengthProperty = lengthProperty;
        }

        private readonly PropertyInfo _arrayLengthProperty;

        public override void Execute(BinaryReader reader, object targetObj)
        {
            //CheckTypes(targetObj);
            var arrayLength = (int)_arrayLengthProperty.GetGetMethod().Invoke(targetObj, null);
            //this is bad...
            var planner = Construct.Planner;
            var arrayElementType = TargetProperty.Property.PropertyType.GetElementType();
            Array array = Array.CreateInstance(arrayElementType, arrayLength);
            if(arrayElementType.IsConstruct())
            {
                var arrayTypePlan = planner.CreateConstructPlan(arrayElementType);
                for(int i = 0; i < arrayLength; i++)
                {
                    var obj = Activator.CreateInstance(arrayElementType);
                    foreach (var planAction in arrayTypePlan.PlanActions)
                    {
                        planAction.Execute(reader, obj);
                    }
                    array.SetValue(obj, i);
                }
            }
            else
            {
                //TODO: evaluate how the design can handle arrays of primitives without repeating code.
                var actions = ReflectionHelper.GetTypeActionMappings();
                if (!actions.ContainsKey(arrayElementType))
                {
                    throw new Exception(
                        string.Format("The enum {0} of base type {1} does not have a handler mapped for the base type.",
                                      TargetProperty.Property.PropertyType, arrayElementType));
                }
                var action = actions[arrayElementType];
                for (int i = 0; i < arrayLength; i++)
                {
                    var planAction = (ConstructPlanAction)Activator.CreateInstance(action.First(), new object[] { TargetProperty }); // we don't need a TargetProperty.... hmmmm
                    var result = planAction.GetValue(reader);
                    array.SetValue(result, i);
                }
            }
            SetterMethod.Invoke(targetObj, new[] { array });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            
        }

        internal override object GetValue(BinaryReader reader)
        {
            // This wont really work for an array, it needs more info (like the length and type)
            // and i want to be able to call this separately to Execute, and the info
            // wont be present when the ctor is called, so i cant set it then.
            throw new NotImplementedException();
        }
    }
}