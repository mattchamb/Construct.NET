using System;
using System.IO;
using System.Reflection;


namespace Construct.NET
{
    [ConstructTarget(typeof(UInt16))]
    public class UInt16Action : ConstructPlanAction
    {
        public UInt16Action(ConstructProperty targetProperty)
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
            var value = (UInt16)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadUInt16();
        }
    }
}