using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Construct.NET
{
    [ConstructTarget(typeof(string))]
    public class AsciiStringAction : ConstructPlanAction
    {
        public AsciiStringAction(ConstructProperty targetProperty)
            : base(targetProperty)
        {
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

        internal override object GetValue(BinaryReader reader)
        {
            List<byte> characters;
            int stringLength = TargetProperty.StringLength;
            if(stringLength > 0)
            {
                characters = ReadFixedLengthString(stringLength, reader);
            }
            else
            {
                characters = ReadNullTerminatedString(reader);
            }
            return Encoding.ASCII.GetString(characters.ToArray());
        }

        private List<byte> ReadNullTerminatedString(BinaryReader reader)
        {
            var result = new List<byte>();
            byte read;
            while((read = reader.ReadByte()) != 0)
            {
                result.Add(read);
            }
            return result;
        }

        private List<byte> ReadFixedLengthString(int stringLength, BinaryReader reader)
        {
            return new List<byte>(reader.ReadBytes(stringLength));
        }
    }
}