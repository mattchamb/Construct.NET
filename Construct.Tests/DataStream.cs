using System;
using System.IO;
using System.Net;

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
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(uint value, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(long value, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(ulong value, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
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
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(ushort value, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(float value, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
        }

        public static Stream Create(double value, ByteOrder byteOrder)
        {
            if (byteOrder == ByteOrder.Network)
                return Create(BitConverter.GetBytes(ToNetworkOrder(value)));
            return Create(BitConverter.GetBytes(value));
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

        private static uint SignedToUnsignedInteger(int value)
        {
            return unchecked((uint)value);
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

        private static int ToNetworkOrder(int value)
        {
            return IPAddress.HostToNetworkOrder(value);
        }

        private static short ToNetworkOrder(short value)
        {
            return IPAddress.HostToNetworkOrder(value);
        }

        private static long ToNetworkOrder(long value)
        {
            return IPAddress.HostToNetworkOrder(value);
        }

        private static uint ToNetworkOrder(uint value)
        {
            return SignedToUnsignedInteger(IPAddress.HostToNetworkOrder(UnsignedToSignedInteger(value)));  
        }

        private static ushort ToNetworkOrder(ushort value)
        {
            return SignedToUnsignedShort(IPAddress.HostToNetworkOrder(UnsignedToSignedShort(value)));
        }

        private static ulong ToNetworkOrder(ulong value)
        {
            return SignedToUnsignedLong(IPAddress.HostToNetworkOrder(UnsignedToSignedLong(value)));
        }

        private static float ToNetworkOrder(float value)
        {
            return IntegerToFloat(IPAddress.HostToNetworkOrder(FloatToInteger(value)));
        }

        private static double ToNetworkOrder(double value)
        {
            return LongToDouble(IPAddress.HostToNetworkOrder(DoubleToLong(value)));
        }
    }
}
