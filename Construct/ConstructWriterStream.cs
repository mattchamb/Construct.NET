using System;
using System.Globalization;
using System.IO;
using Construct.Infrastructure;

namespace Construct
{
    public class ConstructWriterStream : Stream
    {
        private readonly Stream _baseStream;

        public ConstructWriterStream(Stream baseStream)
        {
            Require.NotNull(baseStream, "baseStream");
            Require.That("baseStream", baseStream.CanWrite);
            _baseStream = baseStream;
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

        public void WriteBytes(byte[] value)
        {
            Write(value, 0, value.Length);
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

        private void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public void WriteBoolean(bool value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void WriteSByte(sbyte value)
        {
            WriteByte(unchecked ((byte)value));
        }

        public void WriteInt16(Int16 value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteUInt16(UInt16 value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteInt32(Int32 value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteInt64(Int64 value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteSingle(float value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteDouble(double value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteUInt32(UInt32 value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteUInt64(UInt64 value, ByteOrder byteOrder)
        {
            var outputValue = ByteOrderConverter.ToOutputOrder(value, byteOrder);
            Write(BitConverter.GetBytes(outputValue));
        }

        public void WriteEnum(Enum value, ByteOrder byteOrder)
        {
            var enumType = value.GetType();
            Require.That("value", enumType.IsEnum);
            var enumBaseType = Enum.GetUnderlyingType(enumType);
            
            if (enumBaseType == typeof(byte))
            {
                WriteByte(Convert.ToByte(value, CultureInfo.InvariantCulture));
            }
            else if (enumBaseType == typeof(sbyte))
            {
                WriteSByte(Convert.ToSByte(value, CultureInfo.InvariantCulture));
            }
            else if (enumBaseType == typeof(int))
            {
                WriteInt32(Convert.ToInt32(value, CultureInfo.InvariantCulture), byteOrder);
            }
            else if (enumBaseType == typeof(uint))
            {
                WriteUInt32(Convert.ToUInt32(value, CultureInfo.InvariantCulture), byteOrder);
            }
            else if (enumBaseType == typeof(short))
            {
                WriteInt16(Convert.ToInt16(value, CultureInfo.InvariantCulture), byteOrder);
            }
            else if (enumBaseType == typeof(ushort))
            {
                WriteUInt16(Convert.ToUInt16(value, CultureInfo.InvariantCulture), byteOrder);
            }
            else if (enumBaseType == typeof(long))
            {
                WriteInt64(Convert.ToInt64(value, CultureInfo.InvariantCulture), byteOrder);
            }
            else if (enumBaseType == typeof(ulong))
            {
                WriteUInt64(Convert.ToUInt64(value, CultureInfo.InvariantCulture), byteOrder);
            }
            else
            {
                throw new ArgumentOutOfRangeException("value", "The provided argument did not meet the specified requirements.");
            }
        }
    }
}