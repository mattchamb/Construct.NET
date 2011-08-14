using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Construct.NET.Attributes;

namespace Construct.NET
{

    public abstract class ConstructPlanAction
    {
        public PropertyInfo TargetProperty { get; private set; }
        protected MethodInfo SetterMethod;
        protected MethodInfo GetterMethod;
        public abstract Type TargetType { get; }

        protected ConstructPlanAction(PropertyInfo targetProperty)
        {
            TargetProperty = targetProperty;
            SetterMethod = TargetProperty.GetSetMethod();
            GetterMethod = TargetProperty.GetGetMethod();
        }

        public abstract void Execute(BinaryReader reader, object targetObj);
        public abstract void Output(BinaryWriter writer, object targetObj);

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

        protected internal abstract object GetValue(BinaryReader reader);
    }

    [ConstructTarget(typeof(Int16))]
    public class Int16Action : ConstructPlanAction
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

    [ConstructTarget(typeof(UInt16))]
    public class UInt16Action : ConstructPlanAction
    {
        public UInt16Action(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(UInt16); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            var value = reader.ReadUInt16();
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (UInt16)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadUInt16();
        }
    }

    [ConstructTarget(typeof(Int32))]
    public class Int32Action : ConstructPlanAction
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

            var value = reader.ReadInt32();
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

    [ConstructTarget(typeof(UInt32))]
    public class UInt32Action : ConstructPlanAction
    {
        public UInt32Action(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(UInt32); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            var value = reader.ReadUInt32();
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (UInt32)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadUInt32();
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

            var value = reader.ReadByte();
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (byte)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadByte();
        }
    }

    [ConstructTarget(typeof(float))]
    public class FloatAction : ConstructPlanAction
    {
        public FloatAction(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(float); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            CheckTypes(targetObj);

            var value = reader.ReadSingle();
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (float)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadSingle();
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

            var value = reader.ReadDouble();
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

            var value = reader.ReadChar();
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (char)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            return reader.ReadChar();
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

    [ConstructTarget(typeof(Array))]
    public class ArrayAction : ConstructPlanAction
    {
        public ArrayAction(PropertyInfo lengthProperty, PropertyInfo targetProperty)
            : base(targetProperty)
        {
            if (!(targetProperty.GetSerializationOrder() > lengthProperty.GetSerializationOrder()))
            {
                throw new Exception("Cannot deserialize an array without knowing its length - it must use a property assigned before the array.");
            }
            _arrayLengthProperty = lengthProperty;
        }

        private readonly PropertyInfo _arrayLengthProperty;

        public override Type TargetType
        {
            get { return typeof(Array); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            //CheckTypes(targetObj);
            var arrayLength = (int)_arrayLengthProperty.GetGetMethod().Invoke(targetObj, null);
            var planner = new ConstructPlanner();
            var arrayElementType = TargetProperty.PropertyType.GetElementType();
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
                                      TargetProperty.PropertyType, arrayElementType));
                }
                var action = actions[arrayElementType];
                for (int i = 0; i < arrayLength; i++)
                {
                    var planAction = (ConstructPlanAction)Activator.CreateInstance(action, new object[] { TargetProperty }); // we don't need a TargetProperty.... hmmmm
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

        protected internal override object GetValue(BinaryReader reader)
        {
            // This wont really work for an array, it needs more info (like the length and type)
            // and i want to be able to call this separately to Execute, and the info
            // wont be present when the ctor is called, so i cant set it then.
            throw new NotImplementedException();
        }
    }

    [ConstructTarget(typeof(Enum))]
    public class EnumAction : ConstructPlanAction
    {
        public EnumAction(PropertyInfo targetProperty)
            : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(Enum); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            //CheckTypes(targetObj);
            var enumBaseType = TargetProperty.PropertyType.GetEnumUnderlyingType();
            var actions = ReflectionHelper.GetTypeActionMappings();
            if(!actions.ContainsKey(enumBaseType))
            {
                throw new Exception(
                    string.Format("The enum {0} of base type {1} does not have a handler mapped for the base type.",
                                  TargetProperty.PropertyType, enumBaseType));
            }
            var action = actions[enumBaseType];
            var planAction = (ConstructPlanAction)Activator.CreateInstance(action, new object[] { TargetProperty }); // we don't need a TargetProperty.... hmmmm
            var result = planAction.GetValue(reader);

            bool isFlags = TargetProperty.PropertyType.GetCustomAttributes(typeof(FlagsAttribute), false).Any();

            if (!isFlags && !TargetProperty.PropertyType.IsEnumDefined(result))
            {
                throw new Exception(string.Format("The enum {0} is not defined for the value {1}",
                                                  TargetProperty.PropertyType, result));
            }
            SetterMethod.Invoke(targetObj, new[] { result });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            throw new NotImplementedException();
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }

    [ConstructTarget(typeof(NestedAction))]
    public class NestedAction : ConstructPlanAction
    {
        public NestedAction(PropertyInfo targetProperty) : base(targetProperty)
        {
        }

        public override Type TargetType
        {
            get { return typeof(NestedAction); }
        }

        public override void Execute(BinaryReader reader, object targetObj)
        {
            var obj = GetValue(reader);
            SetterMethod.Invoke(targetObj, new [] { obj });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            //This is bad, shouldn't depend on a concrete type.
            var planner = new ConstructPlanner();
            var nestedTypePlan = planner.CreateConstructPlan(TargetProperty.PropertyType);
            var obj = GetterMethod.Invoke(targetObj, null);
            foreach (var planAction in nestedTypePlan.PlanActions)
            {
                planAction.Output(writer, obj);
            }
        }

        protected internal override object GetValue(BinaryReader reader)
        {
            //This is bad, shouldn't depend on a concrete type.
            var planner = new ConstructPlanner();
            var nestedTypePlan = planner.CreateConstructPlan(TargetProperty.PropertyType);
            var obj = Activator.CreateInstance(TargetProperty.PropertyType);
            foreach (var planAction in nestedTypePlan.PlanActions)
            {
                planAction.Execute(reader, obj);
            }
            return obj;
        }
    }
    

    public class ConstructPlan
    {
        public IList<ConstructPlanAction> PlanActions { get; set; }
    }
}