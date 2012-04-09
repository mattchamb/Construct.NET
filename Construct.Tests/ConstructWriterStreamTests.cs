using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class ConstructWriterStreamTests
    {
        [Test]
        [TestCase(0, ByteOrder.Host)]
        [TestCase(-3453, ByteOrder.Host)]
        [TestCase(3453, ByteOrder.Host)]
        [TestCase(int.MinValue, ByteOrder.Host)]
        [TestCase(int.MaxValue, ByteOrder.Host)]
        [TestCase(0, ByteOrder.Network)]
        [TestCase(-3453, ByteOrder.Network)]
        [TestCase(3453, ByteOrder.Network)]
        [TestCase(int.MinValue, ByteOrder.Network)]
        [TestCase(int.MaxValue, ByteOrder.Network)]
        public void CheckReadWriteInt32ReturnsSameValue(int value, ByteOrder byteOrder)
        {
            using(var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteInt32(value, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadInt32(byteOrder);

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase(0U, ByteOrder.Host)]
        [TestCase(3453U, ByteOrder.Host)]
        [TestCase(uint.MinValue, ByteOrder.Host)]
        [TestCase(uint.MaxValue, ByteOrder.Host)]
        [TestCase(0U, ByteOrder.Network)]
        [TestCase(3453U, ByteOrder.Network)]
        [TestCase(uint.MinValue, ByteOrder.Network)]
        [TestCase(uint.MaxValue, ByteOrder.Network)]
        public void CheckReadWriteUInt32ReturnsSameValue(uint value, ByteOrder byteOrder)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteUInt32(value, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadUInt32(byteOrder);

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase(0L, ByteOrder.Host)]
        [TestCase(3453L, ByteOrder.Host)]
        [TestCase(-3453L, ByteOrder.Host)]
        [TestCase(long.MinValue, ByteOrder.Host)]
        [TestCase(long.MaxValue, ByteOrder.Host)]
        [TestCase(0L, ByteOrder.Network)]
        [TestCase(3453L, ByteOrder.Network)]
        [TestCase(-3453L, ByteOrder.Network)]
        [TestCase(long.MinValue, ByteOrder.Network)]
        [TestCase(long.MaxValue, ByteOrder.Network)]
        public void CheckReadWriteInt64ReturnsSameValue(long value, ByteOrder byteOrder)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteInt64(value, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadInt64(byteOrder);

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase(0UL, ByteOrder.Host)]
        [TestCase(3453UL, ByteOrder.Host)]
        [TestCase(ulong.MinValue, ByteOrder.Host)]
        [TestCase(ulong.MaxValue, ByteOrder.Host)]
        [TestCase(0UL, ByteOrder.Network)]
        [TestCase(3453UL, ByteOrder.Network)]
        [TestCase(ulong.MinValue, ByteOrder.Network)]
        [TestCase(ulong.MaxValue, ByteOrder.Network)]
        public void CheckReadWriteUInt64ReturnsSameValue(ulong value, ByteOrder byteOrder)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteUInt64(value, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadUInt64(byteOrder);

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase(0, ByteOrder.Host)]
        [TestCase(3453, ByteOrder.Host)]
        [TestCase(short.MinValue, ByteOrder.Host)]
        [TestCase(short.MaxValue, ByteOrder.Host)]
        [TestCase(0, ByteOrder.Network)]
        [TestCase(3453, ByteOrder.Network)]
        [TestCase(short.MinValue, ByteOrder.Network)]
        [TestCase(short.MaxValue, ByteOrder.Network)]
        public void CheckReadWriteInt16ReturnsSameValue(short value, ByteOrder byteOrder)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteInt16(value, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadInt16(byteOrder);

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase((ushort)0, ByteOrder.Host)]
        [TestCase((ushort)3453, ByteOrder.Host)]
        [TestCase(ushort.MinValue, ByteOrder.Host)]
        [TestCase(ushort.MaxValue, ByteOrder.Host)]
        [TestCase((ushort)0, ByteOrder.Network)]
        [TestCase((ushort)3453, ByteOrder.Network)]
        [TestCase(ushort.MinValue, ByteOrder.Network)]
        [TestCase(ushort.MaxValue, ByteOrder.Network)]
        public void CheckReadWriteUInt16ReturnsSameValue(ushort value, ByteOrder byteOrder)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteUInt16(value, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadUInt16(byteOrder);

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase((byte)34)]
        [TestCase(byte.MinValue)]
        [TestCase(byte.MaxValue)]
        public void CheckReadWriteByteReturnsSameValue(byte value)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteByte(value);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadByte();

                Assert.AreEqual(value, result);
            }
        }

        [Test]
        [TestCase((sbyte)0)]
        [TestCase((sbyte)34)]
        [TestCase((sbyte)-34)]
        [TestCase(sbyte.MinValue)]
        [TestCase(sbyte.MaxValue)]
        public void CheckReadWriteSByteReturnsSameValue(sbyte value)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteSByte(value);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadSByte();

                Assert.AreEqual(value, result);
            }
        }

        public enum ULongEnum : ulong
        {
            ValueOne = 1235345634UL,
            ValueTwo = ulong.MinValue,
            ValueThree = ulong.MaxValue,
        }

        [Test]
        [TestCase(ULongEnum.ValueOne, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueTwo, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueThree, ByteOrder.Host)]
        [TestCase(ULongEnum.ValueOne, ByteOrder.Network)]
        [TestCase(ULongEnum.ValueTwo, ByteOrder.Network)]
        [TestCase(ULongEnum.ValueThree, ByteOrder.Network)]
        public void ULongEnumsAreWrittenAndReadCorrectly(ULongEnum enumValue, ByteOrder byteOrder)
        {
            using (var baseStream = new MemoryStream())
            {
                var constructWriter = new ConstructWriterStream(baseStream);
                constructWriter.WriteEnum(enumValue, byteOrder);

                baseStream.Seek(0, SeekOrigin.Begin);

                var constructReader = new ConstructReaderStream(baseStream);
                var result = constructReader.ReadEnum(typeof(ULongEnum), byteOrder);

                Assert.AreEqual(enumValue, result);
            }
        }
    }
}
