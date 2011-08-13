using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Construct.NET.Attributes;

namespace Construct.NET
{

    public abstract class ConstructPlanAction
    {
        public PropertyInfo TargetProperty { get; private set; }

        public abstract Type TargetType { get; }

        protected ConstructPlanAction(PropertyInfo targetProperty)
        {
            TargetProperty = targetProperty;
        }

        public abstract void Execute(BinaryReader reader, object targetObject);


        protected void CheckTypes(object targetObj)
        {
            if (!targetObj.GetType().Equals(TargetProperty.DeclaringType))
            {
                throw new ArgumentException(
                    string.Format("The property \"{0}\" does not belong to the type {1}",
                                  TargetProperty, targetObj.GetType()));
            }
            if (!TargetProperty.PropertyType.Equals(TargetType))
            {
                throw new Exception(
                    string.Format("PropertyType \"{0}\" does not match the TargetType \"{1}\"",
                                                  TargetProperty, TargetType));
            }
        }


    }

    [ConstructTarget(typeof(int))]
    public class IntAction : ConstructPlanAction
    {
        public IntAction(PropertyInfo targetProperty) 
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof (int); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            int value = reader.ReadInt32();
            var setter = TargetProperty.GetSetMethod();
            setter.Invoke(targetObj, new object[] {value});
        }
    }

    [ConstructTarget(typeof(byte))]
    public class ByteAction : ConstructPlanAction
    {
        public ByteAction(PropertyInfo targetProperty) 
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(byte); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            byte value = reader.ReadByte();
            var setter = TargetProperty.GetSetMethod();
            setter.Invoke(targetObj, new object[] { value });
        }
    }

    [ConstructTarget(typeof(double))]
    public class DoubleAction : ConstructPlanAction
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

            double value = reader.ReadDouble();
            var setter = TargetProperty.GetSetMethod();
            setter.Invoke(targetObj, new object[] { value });
        }
    }

    [ConstructTarget(typeof(char))]
    public class CharAction : ConstructPlanAction
    {
        public CharAction(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(char); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            char value = reader.ReadChar();
            var setter = TargetProperty.GetSetMethod();
            setter.Invoke(targetObj, new object[] { value });
        }
    }

    [ConstructTarget(typeof(string))]
    public class AsciiStringAction : ConstructPlanAction
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

            var characters = new List<byte>();
            byte read;
            while(reader.BaseStream.CanRead && (read = reader.ReadByte()) != 0)
            {
                characters.Add(read);
            }
            var value = Encoding.ASCII.GetString(characters.ToArray());
            var setter = TargetProperty.GetSetMethod();
            setter.Invoke(targetObj, new object[] { value });
        }
    }
    

    public class ConstructPlan
    {
        public IList<ConstructPlanAction> PlanActions { get; set; }
    }
}