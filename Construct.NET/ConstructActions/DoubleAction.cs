using System;
using System.IO;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(double))]
    internal class DoubleAction : ConstructPlanAction
    {
        public DoubleAction(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(double); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            var value = GetValue(reader);
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (double)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadDouble();
        }
    }
}