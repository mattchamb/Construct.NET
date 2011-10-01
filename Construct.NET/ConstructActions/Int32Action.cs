using System;
using System.IO;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(Int32))]
    internal class Int32Action : ConstructPlanAction
    {
        public Int32Action(PropertyInfo targetProperty) 
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof (Int32); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            var value = GetValue(reader);
            SetterMethod.Invoke(targetObj, new object[] {value});
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (Int32)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadInt32();
        }
    }
}