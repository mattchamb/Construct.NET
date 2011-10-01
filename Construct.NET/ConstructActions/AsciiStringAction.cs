using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;


namespace Construct.NET
{
    [ConstructTarget(typeof(string))]
    internal class AsciiStringAction : ConstructPlanAction
    {
        public AsciiStringAction(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(string); }
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
            var value = (string)GetterMethod.Invoke(targetObj, null);
            var stringBytes = Encoding.ASCII.GetBytes(value);
            writer.Write(stringBytes);
            writer.Write((byte)0);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            var characters = new List<byte>();
            byte read;
            while (reader.BaseStream.CanRead && (read = reader.ReadByte()) != 0)
            {
                characters.Add(read);
            }
            return Encoding.ASCII.GetString(characters.ToArray());
        }
    }
}