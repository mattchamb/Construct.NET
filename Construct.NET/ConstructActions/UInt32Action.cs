using System;
using System.IO;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(UInt32))]
    public class UInt32Action : ConstructPlanAction
    {
        public UInt32Action(ConstructProperty targetProperty)
            : base(targetProperty)
        {
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
            var value = (UInt32)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadUInt32();
        }
    }
}