using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Construct.NET
{
    public enum CtrType
    {
        Custom,
        Flags,
        Byte,
        SByte,
        Char,
        Decimal,
        Double,
        Float,
        Int32,
        UInt32,
        Int64,
        UInt64,
        Int16,
        UInt16,
        String
    }

    public class DynamicConstruct : DynamicObject
    {
        private class ConstructProperty
        {
            public string Name { get; set; }
            public CtrType Type { get; set; }
            public object Value { get; set; }
        }

        public delegate object TypeReader(BinaryReader reader);

        private readonly List<ConstructProperty> _properties;
        private readonly Dictionary<CtrType, TypeReader> _readerFunctions;
        private bool _hasParsed = false;

        public DynamicConstruct()
        {
            _properties = new List<ConstructProperty>();
            _readerFunctions = new Dictionary<CtrType, TypeReader>
                                   {
                                       {CtrType.Byte, x => x.ReadByte()},
                                       {CtrType.SByte, x => x.ReadSByte()},
                                       {CtrType.Char, x => x.ReadChar()},
                                       {CtrType.Decimal, x => x.ReadDecimal()},
                                       {CtrType.Double, x => x.ReadDouble()},
                                       {CtrType.Float, x => x.ReadSingle()},
                                       {CtrType.Int32, x => x.ReadInt32()},
                                       {CtrType.UInt32, x => x.ReadUInt32()},
                                       {CtrType.Int64, x => x.ReadInt64()},
                                       {CtrType.UInt64, x => x.ReadUInt16()},
                                       {CtrType.Int16, x => x.ReadInt16()},
                                       {CtrType.UInt16, x => x.ReadInt16()}
                                   };
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!base.TrySetMember(binder, value))
            {
                var propertyType = (CtrType)value;
                if (propertyType == CtrType.Custom || propertyType == CtrType.Flags || propertyType == CtrType.String)
                    throw new Exception("Custom Types, strings and enums not yet implemented");
                _properties.Add(new ConstructProperty { Name = binder.Name, Type = propertyType });
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!_hasParsed)
                return base.TryGetMember(binder, out result);

            if (!base.TryGetMember(binder, out result))
            {
                var property = _properties.FirstOrDefault(x => x.Name == binder.Name);
                if (property == null)
                    return false;
                result = property.Value;
            }
            return true;
        }

        public DynamicConstruct Parse(Stream inputStream)
        {
            var binaryReader = new BinaryReader(inputStream);

            foreach (var constructProperty in _properties)
            {
                if (_readerFunctions.ContainsKey(constructProperty.Type))
                {
                    constructProperty.Value = _readerFunctions[constructProperty.Type](binaryReader);
                }
            }
            _hasParsed = true;
            return this;
        }


    }
}
