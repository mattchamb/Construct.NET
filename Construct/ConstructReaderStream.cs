using System;
using System.IO;
using Construct.Exceptions;
using Construct.Infrastructure;

namespace Construct
{
    public class ConstructReaderStream : Stream
    {
        private readonly Stream _baseStream;

        public ConstructReaderStream(Stream baseStream)
        {
            Require.NotNull(baseStream, "baseStream");
            Require.That("baseStream", baseStream.CanRead);
            _baseStream = baseStream;
        }

        public bool ReadBoolean()
        {
            var data = ReadBytes(1);
            return BitConverter.ToBoolean(data, 0);
        }

        public new byte ReadByte()
        {
            var data = ReadBytes(1);
            return data[0];
        }

        public sbyte ReadSByte()
        {
            var value = ReadByte();
            return unchecked((sbyte)value);
        }

        public short ReadInt16(ByteOrder byteOrder)
        {
            var data = ReadBytes(2);
            var value = BitConverter.ToInt16(data, 0);
            return ByteOrderConverter.ToHostOrder(value, byteOrder);
        }

        public ushort ReadUInt16(ByteOrder byteOrder)
        {
            var data = ReadBytes(2);
            var value = BitConverter.ToUInt16(data, 0);
            return ByteOrderConverter.ToHostOrder(value, byteOrder);
        }

        public int ReadInt32(ByteOrder byteOrder)
        {
            var data = ReadBytes(4);
            var value = BitConverter.ToInt32(data, 0);
            return ByteOrderConverter.ToHostOrder(value, byteOrder);
        }

        public Enum ReadEnum(Type enumType, ByteOrder byteOrder)
        {
            Require.NotNull(enumType, "enumType");
            Require.That("enumType", enumType.IsEnum);

            var enumBaseType = enumType.GetEnumUnderlyingType();
            if (enumBaseType == typeof(byte))
            {
                var value = ReadByte();
                return (Enum)Enum.ToObject(enumType, value);
            }
            if (enumBaseType == typeof(sbyte))
            {
                var value = ReadSByte();
                return (Enum)Enum.ToObject(enumType, value);
            }
            if(enumBaseType == typeof(int))
            {
                var value = ReadInt32(byteOrder);
                return (Enum)Enum.ToObject(enumType, value);
            }
            if (enumBaseType == typeof(uint))
            {
                var value = ReadUInt32(byteOrder);
                return (Enum)Enum.ToObject(enumType, value);
            }
            if (enumBaseType == typeof(short))
            {
                var value = ReadInt16(byteOrder);
                return (Enum)Enum.ToObject(enumType, value);
            }
            if (enumBaseType == typeof(ushort))
            {
                var value = ReadUInt16(byteOrder);
                return (Enum)Enum.ToObject(enumType, value);
            }
            if (enumBaseType == typeof(long))
            {
                var value = ReadInt64(byteOrder);
                return (Enum)Enum.ToObject(enumType, value);
            }
            if (enumBaseType == typeof(ulong))
            {
                var value = ReadUInt64(byteOrder);
                return (Enum)Enum.ToObject(enumType, value);
            }
            throw new ArgumentOutOfRangeException("enumType", "The provided argument did not meet the specified requirements.");
        }

        public uint ReadUInt32(ByteOrder byteOrder)
        {
            var data = ReadBytes(4);
            var value = BitConverter.ToUInt32(data, 0);
            return ByteOrderConverter.ToHostOrder(value, byteOrder);
        }

        public long ReadInt64(ByteOrder byteOrder)
        {
            var data = ReadBytes(8);
            var value = BitConverter.ToInt64(data, 0);
            return ByteOrderConverter.ToHostOrder(value, byteOrder);
        }

        public ulong ReadUInt64(ByteOrder byteOrder)
        {
            var data = ReadBytes(8);
            var value = BitConverter.ToUInt64(data, 0);
            return ByteOrderConverter.ToHostOrder(value, byteOrder);
        }

        public float ReadSingle(ByteOrder byteOrder)
        {
            var data = ReadBytes(4);
            var value = BitConverter.ToInt32(data, 0);
            value = ByteOrderConverter.ToHostOrder(value, byteOrder);
            return ByteOrderConverter.IntegerToFloat(value);
        }

        public double ReadDouble(ByteOrder byteOrder)
        {
            var data = ReadBytes(8);
            var value = BitConverter.ToInt64(data, 0);
            value = ByteOrderConverter.ToHostOrder(value, byteOrder);
            return ByteOrderConverter.LongToDouble(value);
        }

        public byte[] ReadBytes(int count)
        {
            Require.That("count", count > 0);
            var data = new byte[count];
            int readSoFar = 0;
            while(readSoFar != count)
            {
                int read = Read(data, readSoFar, count - readSoFar);
                readSoFar += read;
                if(read == 0)
                {
                    throw new NotEnoughDataException("The provided stream ran out of data while trying to perform a read.");
                }
            }
            return data;
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _baseStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _baseStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return _baseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _baseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _baseStream.CanWrite; }
        }

        public override long Length
        {
            get { return _baseStream.Length; }
        }

        public override long Position
        {
            get { return _baseStream.Position; }
            set { _baseStream.Position = value; }
        }
    }
}
