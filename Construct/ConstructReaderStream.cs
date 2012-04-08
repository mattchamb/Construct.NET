using System;
using System.IO;
using System.Net;

namespace Construct
{
    public class ConstructReaderStream : Stream
    {
        private readonly Stream _baseStream;

        public ConstructReaderStream(Stream baseStream)
        {
            baseStream.RequireNotNull("baseStream");
            baseStream.Require("baseStream", stream => stream.CanRead);
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
            return ToHostOrder(value, byteOrder);
        }

        public ushort ReadUInt16(ByteOrder byteOrder)
        {
            var data = ReadBytes(2);
            var value = BitConverter.ToUInt16(data, 0);
            return ToHostOrder(value, byteOrder);
        }

        public int ReadInt32(ByteOrder byteOrder)
        {
            var data = ReadBytes(4);
            var value = BitConverter.ToInt32(data, 0);
            return ToHostOrder(value, byteOrder);
        }

        public Enum ReadEnum(Type enumType, ByteOrder byteOrder)
        {
            enumType.RequireNotNull("enumType");
            enumType.Require("enumType", t => t.IsEnum);

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
            return ToHostOrder(value, byteOrder);
        }

        public long ReadInt64(ByteOrder byteOrder)
        {
            var data = ReadBytes(8);
            var value = BitConverter.ToInt64(data, 0);
            return ToHostOrder(value, byteOrder);
        }

        public ulong ReadUInt64(ByteOrder byteOrder)
        {
            var data = ReadBytes(8);
            var value = BitConverter.ToUInt64(data, 0);
            return ToHostOrder(value, byteOrder);
        }

        public float ReadSingle(ByteOrder byteOrder)
        {
            var data = ReadBytes(4);
            var value = BitConverter.ToInt32(data, 0);
            value = ToHostOrder(value, byteOrder);
            return IntegerToFloat(value);
        }

        public double ReadDouble(ByteOrder byteOrder)
        {
            var data = ReadBytes(8);
            var value = BitConverter.ToInt64(data, 0);
            value = ToHostOrder(value, byteOrder);
            return LongToDouble(value);
        }

        private static uint SignedToUnsignedInteger(int value)
        {
            return unchecked ((uint)value);
        }

        private static int UnsignedToSignedInteger(uint value)
        {
            return unchecked((int)value);
        }

        private static ushort SignedToUnsignedShort(short value)
        {
            return unchecked((ushort)value);
        }

        private static short UnsignedToSignedShort(ushort value)
        {
            return unchecked((short)value);
        }

        private static ulong SignedToUnsignedLong(long value)
        {
            return unchecked((ulong)value);
        }

        private static long UnsignedToSignedLong(ulong value)
        {
            return unchecked((long)value);
        }

        private static int FloatToInteger(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes, 0);
        }

        private static float IntegerToFloat(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToSingle(bytes, 0);
        }

        private static long DoubleToLong(double value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }

        private static double LongToDouble(long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }

        private byte[] ReadBytes(int count)
        {
            count.Require("count", arg => arg > 0);
            var data = new byte[count];
            int readSoFar = 0;
            while(readSoFar != count)
            {
                int read = _baseStream.Read(data, readSoFar, count - readSoFar);
                readSoFar += read;
                if(read == 0)
                {
                    throw new NotEnoughDataException("The provided stream ran out of data while trying to perform a read.");
                }
            }
            return data;
        }

        protected static int ToHostOrder(int value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IPAddress.NetworkToHostOrder(value);
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static short ToHostOrder(short value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IPAddress.NetworkToHostOrder(value);
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static long ToHostOrder(long value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IPAddress.NetworkToHostOrder(value);
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static uint ToHostOrder(uint value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return SignedToUnsignedInteger(IPAddress.NetworkToHostOrder(UnsignedToSignedInteger(value)));
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static ushort ToHostOrder(ushort value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return SignedToUnsignedShort(IPAddress.NetworkToHostOrder(UnsignedToSignedShort(value)));
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static ulong ToHostOrder(ulong value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return SignedToUnsignedLong(IPAddress.NetworkToHostOrder(UnsignedToSignedLong(value)));
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static float ToHostOrder(float value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IntegerToFloat(IPAddress.NetworkToHostOrder(FloatToInteger(value)));
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
        }

        protected static double ToHostOrder(double value, ByteOrder inputByteOrder)
        {
            switch (inputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return LongToDouble(IPAddress.NetworkToHostOrder(DoubleToLong(value)));
                default:
                    throw new ArgumentOutOfRangeException("inputByteOrder");
            }
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
