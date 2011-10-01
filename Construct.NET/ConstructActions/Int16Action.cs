using System;
using System.IO;
using System.Reflection;

namespace Construct.NET
{
    [ConstructTarget(typeof(Int16))]
    internal class Int16Action : ConstructPlanAction
    {
        public Int16Action(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(Int16); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            var value = GetValue(reader);
            SetterMethod.Invoke(targetObj, new [] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (Int16)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadInt16();
        }
    }
}