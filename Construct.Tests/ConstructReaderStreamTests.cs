using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class ConstructReaderStreamTests
    {
        [Test]
        public void NotEnoughDataIsThrownWhenNotEnoughData()
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(123));
            Assert.Throws<NotEnoughDataException>(() => constructStream.ReadInt64(ByteOrder.Host));
        }

        [Test]
        [TestCase(88)]
        [TestCase(Byte.MaxValue)]
        [TestCase(Byte.MinValue)]
        public void ReadByteIsCorrectValue(byte value)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value));

            Assert.AreEqual(value, constructStream.ReadByte());
        }

        [Test]
        [TestCase(0)]
        [TestCase(88)]
        [TestCase(SByte.MaxValue)]
        [TestCase(SByte.MinValue)]
        public void ReadSByteIsCorrectValue(sbyte value)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value));

            Assert.AreEqual(value, constructStream.ReadSByte());
        }

        [Test]
        [TestCase(0, ByteOrder.Host)]
        [TestCase(88, ByteOrder.Host)]
        [TestCase(int.MaxValue, ByteOrder.Host)]
        [TestCase(int.MinValue, ByteOrder.Host)]
        [TestCase(0, ByteOrder.Network)]
        [TestCase(88, ByteOrder.Network)]
        [TestCase(int.MaxValue, ByteOrder.Network)]
        [TestCase(int.MinValue, ByteOrder.Network)]
        public void ReadIntIsCorrectValue(int value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadInt32(byteOrder));
        }

        [Test]
        [TestCase((uint)88, ByteOrder.Host)]
        [TestCase(uint.MaxValue, ByteOrder.Host)]
        [TestCase(uint.MinValue, ByteOrder.Host)]
        [TestCase((uint)88, ByteOrder.Network)]
        [TestCase(uint.MaxValue, ByteOrder.Network)]
        [TestCase(uint.MinValue, ByteOrder.Network)]
        public void ReadUIntIsCorrectValue(uint value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadUInt32(byteOrder));
        }

        [Test]
        [TestCase((short)0, ByteOrder.Host)]
        [TestCase((short)88, ByteOrder.Host)]
        [TestCase(short.MaxValue, ByteOrder.Host)]
        [TestCase(short.MinValue, ByteOrder.Host)]
        [TestCase((short)0, ByteOrder.Network)]
        [TestCase((short)88, ByteOrder.Network)]
        [TestCase(short.MaxValue, ByteOrder.Network)]
        [TestCase(short.MinValue, ByteOrder.Network)]
        public void ReadShortIsCorrectValue(short value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadInt16(byteOrder));
        }

        [Test]
        [TestCase((ushort)88, ByteOrder.Host)]
        [TestCase(ushort.MaxValue, ByteOrder.Host)]
        [TestCase(ushort.MinValue, ByteOrder.Host)]
        [TestCase((ushort)88, ByteOrder.Network)]
        [TestCase(ushort.MaxValue, ByteOrder.Network)]
        [TestCase(ushort.MinValue, ByteOrder.Network)]
        public void ReadUShortIsCorrectValue(ushort value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadUInt16(byteOrder));
        }

        [Test]
        [TestCase((long)0, ByteOrder.Host)]
        [TestCase((long)88, ByteOrder.Host)]
        [TestCase(long.MaxValue, ByteOrder.Host)]
        [TestCase(long.MinValue, ByteOrder.Host)]
        [TestCase((long)0, ByteOrder.Network)]
        [TestCase((long)88, ByteOrder.Network)]
        [TestCase(long.MaxValue, ByteOrder.Network)]
        [TestCase(long.MinValue, ByteOrder.Network)]
        public void ReadLongIsCorrectValue(long value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadInt64(byteOrder));
        }

        [Test]
        [TestCase((ulong)88, ByteOrder.Host)]
        [TestCase(ulong.MaxValue, ByteOrder.Host)]
        [TestCase(ulong.MinValue, ByteOrder.Host)]
        [TestCase((ulong)88, ByteOrder.Network)]
        [TestCase(ulong.MaxValue, ByteOrder.Network)]
        [TestCase(ulong.MinValue, ByteOrder.Network)]
        public void ReadULongIsCorrectValue(ulong value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadUInt64(byteOrder));
        }

        [Test]
        [TestCase(0.0f, ByteOrder.Host)]
        [TestCase(88.4f, ByteOrder.Host)]
        [TestCase(float.MaxValue, ByteOrder.Host)]
        [TestCase(float.MinValue, ByteOrder.Host)]
        [TestCase(0.0f, ByteOrder.Network)]
        [TestCase(88.4f, ByteOrder.Network)]
        [TestCase(float.MaxValue, ByteOrder.Network)]
        [TestCase(float.MinValue, ByteOrder.Network)]
        public void ReadFloatIsCorrectValue(float value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadSingle(byteOrder));
        }

        [Test]
        [TestCase(0.0D, ByteOrder.Host)]
        [TestCase(88.0D, ByteOrder.Host)]
        [TestCase(double.MaxValue, ByteOrder.Host)]
        [TestCase(double.MinValue, ByteOrder.Host)]
        [TestCase(0.0D, ByteOrder.Network)]
        [TestCase(88.0D, ByteOrder.Network)]
        [TestCase(double.MaxValue, ByteOrder.Network)]
        [TestCase(double.MinValue, ByteOrder.Network)]
        public void ReadDoubleIsCorrectValue(double value, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create(value, byteOrder));

            Assert.AreEqual(value, constructStream.ReadDouble(byteOrder));
        }

        public enum IntegerEnum : int
        {
            ValueOne = 0,
            ValueTwo = 3453,
            ValueThree = int.MaxValue,
            ValueFour = int.MinValue
        }

        [Test]
        [TestCase(IntegerEnum.ValueOne, ByteOrder.Host)]
        [TestCase(IntegerEnum.ValueTwo, ByteOrder.Host)]
        [TestCase(IntegerEnum.ValueThree, ByteOrder.Host)]
        [TestCase(IntegerEnum.ValueFour, ByteOrder.Host)]
        [TestCase(IntegerEnum.ValueOne, ByteOrder.Network)]
        [TestCase(IntegerEnum.ValueTwo, ByteOrder.Network)]
        [TestCase(IntegerEnum.ValueThree, ByteOrder.Network)]
        [TestCase(IntegerEnum.ValueFour, ByteOrder.Network)]
        public void IntegerEnumsAreReadCorrectly(IntegerEnum enumValue, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create((int)enumValue, byteOrder));
            var result = constructStream.ReadEnum(typeof (IntegerEnum), byteOrder);
            Assert.AreEqual(enumValue, result);
        }

        public enum ByteEnum : byte
        {
            ValueOne = 1,
            ValueTwo = 14,
            ValueThree = byte.MaxValue,
            ValueFour = byte.MinValue
        }

        [Test]
        [TestCase(ByteEnum.ValueOne, ByteOrder.Host)]
        [TestCase(ByteEnum.ValueTwo, ByteOrder.Host)]
        [TestCase(ByteEnum.ValueThree, ByteOrder.Host)]
        [TestCase(ByteEnum.ValueFour, ByteOrder.Host)]
        [TestCase(ByteEnum.ValueOne, ByteOrder.Network)]
        [TestCase(ByteEnum.ValueTwo, ByteOrder.Network)]
        [TestCase(ByteEnum.ValueThree, ByteOrder.Network)]
        [TestCase(ByteEnum.ValueFour, ByteOrder.Network)]
        public void ByteEnumsAreReadCorrectly(ByteEnum enumValue, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create((byte)enumValue));
            var result = constructStream.ReadEnum(typeof(ByteEnum), byteOrder);
            Assert.AreEqual(enumValue, result);
        }


        public enum ULongEnum : ulong
        {
            ValueOne = 1,
            ValueTwo = 14,
            ValueThree = ulong.MaxValue,
            ValueFour = ulong.MinValue
        }

        [Test]
        [TestCase(ULongEnum.ValueOne, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueTwo, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueThree, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueFour, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueOne, ByteOrder.Network)]
        [TestCase(ULongEnum.ValueTwo, ByteOrder.Network)]
        [TestCase(ULongEnum.ValueThree, ByteOrder.Network)]
        [TestCase(ULongEnum.ValueFour, ByteOrder.Network)]
        public void ULongEnumsAreReadCorrectly(ULongEnum enumValue, ByteOrder byteOrder)
        {
            var constructStream = new ConstructReaderStream(DataStream.Create((ulong)enumValue, byteOrder));
            var result = constructStream.ReadEnum(typeof(ULongEnum), byteOrder);
            Assert.AreEqual(enumValue, result);
        }
    }
}
