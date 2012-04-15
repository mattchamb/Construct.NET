using System;
using System.IO;
using System.Text;
using Construct.Infrastructure;

namespace Construct.Tests
{
    static class DataStream
    {
        public static Stream Create(byte[] data)
        {
            return new MemoryStream(data);
        }

        public static Stream Create(int value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(uint value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(long value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(ulong value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(byte value)
        {
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(sbyte value)
        {
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(short value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(ushort value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(float value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        public static Stream Create(double value, ByteOrder byteOrder)
        {
            return Create(BitConverter.GetBytes(ByteOrderConverter.ToOutputOrder(value, byteOrder)));
        }

        internal static Stream Create(string value, Encoding encoding)
        {
            return Create(encoding.GetBytes(value));
        }
    }
}
