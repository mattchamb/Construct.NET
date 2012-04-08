﻿using System;
using System.Linq;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class LambdaGeneratorTests
    {
        class PrimitivePropertyClass
        {
            public int Integer { get; set; } 
        }
        [Test]
        public void AssignmentLambdaAssignsPrimitivesToProperty()
        {
            var primitiveClass = new PrimitivePropertyClass()
                                     {
                                         Integer = 123
                                     };
            var propertyInfo = primitiveClass.GetType().GetProperties().Single();
            var function = LambdaGenerator.CreateAssignmentFunction<PrimitivePropertyClass, int>(propertyInfo);

            function(primitiveClass, 999);

            Assert.AreEqual(999, primitiveClass.Integer);
        }

        class CompositePropertyClass
        {
            public PrimitivePropertyClass PropertyClass { get; set; } 
        }
        [Test]
        public void AssignmentLambdaAssignsCompoundTypeToProperty()
        {
            var compoundClass = new CompositePropertyClass();
            var primitiveClass = new PrimitivePropertyClass()
                                     {
                                         Integer = 123
                                     };
            var propertyInfo = compoundClass.GetType().GetProperties().Single();
            var function = LambdaGenerator.CreateAssignmentFunction<CompositePropertyClass, PrimitivePropertyClass>(propertyInfo);

            function(compoundClass, primitiveClass);

            Assert.AreEqual(primitiveClass, compoundClass.PropertyClass);
            Assert.AreEqual(123, compoundClass.PropertyClass.Integer);
        }


        enum IntegerEnum : int
        {
            ValueOne,
            ValueTwo
        }

        class IntegerEnumClass
        {
            public IntegerEnum Enum { get; set; } 
        }

        [Test]
        public void EnumLambdaAssignsToIntegerEnumProperty()
        {
            var obj = new IntegerEnumClass()
                          {
                              Enum = IntegerEnum.ValueOne
                          };
            var propertyInfo = obj.GetType().GetProperties().Single();
            var function = LambdaGenerator.CreateAssignmentFunctionWithCast<IntegerEnumClass, Enum>(propertyInfo);

            function(obj, IntegerEnum.ValueTwo);

            Assert.AreEqual(IntegerEnum.ValueTwo, obj.Enum);
        }

        enum ByteEnum : byte
        {
            ValueOne,
            ValueTwo
        }

        class ByteEnumClass
        {
            public ByteEnum Enum { get; set; }
        }

        [Test]
        public void EnumLambdaAssignsToByteEnumProperty()
        {
            var obj = new ByteEnumClass()
            {
                Enum = ByteEnum.ValueOne
            };
            var propertyInfo = obj.GetType().GetProperties().Single();
            var function = LambdaGenerator.CreateAssignmentFunctionWithCast<ByteEnumClass, Enum>(propertyInfo);

            function(obj, ByteEnum.ValueTwo);

            Assert.AreEqual(ByteEnum.ValueTwo, obj.Enum);
        }

        enum SByteEnum : sbyte
        {
            ValueOne = 6,
            ValueTwo = -5
        }

        class SByteEnumClass
        {
            public SByteEnum Enum { get; set; }
        }

        [Test]
        public void EnumLambdaAssignsToSByteEnumProperty()
        {
            var obj = new SByteEnumClass()
            {
                Enum = SByteEnum.ValueOne
            };
            var propertyInfo = obj.GetType().GetProperties().Single();
            var function = LambdaGenerator.CreateAssignmentFunctionWithCast<SByteEnumClass, Enum>(propertyInfo);

            function(obj, SByteEnum.ValueTwo);

            Assert.AreEqual(SByteEnum.ValueTwo, obj.Enum);
        }

        enum ULongEnum : ulong
        {
            ValueOne,
            ValueTwo
        }

        class ULongEnumClass
        {
            public ULongEnum Enum { get; set; }
        }

        [Test]
        public void EnumLambdaAssignsToUlongEnumProperty()
        {
            var obj = new ULongEnumClass()
            {
                Enum = ULongEnum.ValueOne
            };
            var propertyInfo = obj.GetType().GetProperties().Single();
            var function = LambdaGenerator.CreateAssignmentFunctionWithCast<ULongEnumClass, Enum>(propertyInfo);

            function(obj, ULongEnum.ValueTwo);

            Assert.AreEqual(ULongEnum.ValueTwo, obj.Enum);
        }
    }
}
