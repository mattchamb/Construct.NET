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

            var value = reader.ReadInt16();
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (Int16)GetterMethod.Invoke(targetObj, null);
            writer.Write(value);
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
            SetterMethod.Invoke(targetObj, new object[] { value });
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            var value = (string)GetterMethod.Invoke(targetObj, null);
            var stringBytes = Encoding.ASCII.GetBytes(value);
            writer.Write(stringBytes);
            writer.Write((byte)0);
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
                SetterMethod.Invoke(targetObj, new [] {array});
            }
            else
            {
                //TODO: evaluate how the design can handle arrays of primitives without repeating code.
                //var actionTypes = typeof(ConstructPlanAction).GetDerivedTypes();
                //var actions = actionTypes.ToDictionary(action => action.GetTarget());
                //for (int i = 0; i < arrayLength; i++)
                //{
                //    var obj = Activator.CreateInstance(arrayElementType);
                //    
                //    planAction.Execute(reader, obj);
                //    array.SetValue(obj, i);
                //}
            }
        }

        public override void Output(BinaryWriter writer, object targetObj)
        {
            CheckTypes(targetObj);
            
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
            //This is bad, shouldn't depend on a concrete type.
            var planner = new ConstructPlanner();
            var nestedTypePlan = planner.CreateConstructPlan(TargetProperty.PropertyType);
            var obj = Activator.CreateInstance(TargetProperty.PropertyType);
            foreach (var planAction in nestedTypePlan.PlanActions)
            {
                planAction.Execute(reader, obj);
            }
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
    }
    

    public class ConstructPlan
    {
        public IList<ConstructPlanAction> PlanActions { get; set; }
    }
}