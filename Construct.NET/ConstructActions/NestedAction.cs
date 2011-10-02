using System;
using System.IO;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(NestedAction))]
    public class NestedAction : ConstructPlanAction
    {
        public NestedAction(ConstructProperty targetProperty)
            : base(targetProperty)
        {
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            var obj = GetValue(reader);
            SetterMethod.Invoke(targetObj, new [] { obj });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            //This is bad, shouldn't depend on the static Construct.
            var planner = Construct.Planner;
            var nestedTypePlan = planner.CreateConstructPlan(TargetProperty.Property.PropertyType);
            var obj = GetterMethod.Invoke(targetObj, null);
            foreach (var planAction in nestedTypePlan.PlanActions)
            {
                planAction.Output(writer, obj);
            }
        }

        internal override object GetValue(BinaryReader reader)
        {
            //This is bad, shouldn't depend on the static Construct.
            var planner = Construct.Planner;
            var nestedTypePlan = planner.CreateConstructPlan(TargetProperty.Property.PropertyType);
            var obj = Activator.CreateInstance(TargetProperty.Property.PropertyType);
            foreach (var planAction in nestedTypePlan.PlanActions)
            {
                planAction.Execute(reader, obj);
            }
            return obj;
        }
    }
}