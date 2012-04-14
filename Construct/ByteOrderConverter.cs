using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Construct
{
    public static class ByteOrderConverter
    {
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

        private static int FloatToInteger(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToInt32(bytes, 0);
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "We are converting between types, so the name should say what types we are converting.")]
        public static float IntegerToFloat(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToSingle(bytes, 0);
        }

        private static long DoubleToLong(double value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }

        public static double LongToDouble(long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }

        public static int ToHostOrder(int value, ByteOrder inputByteOrder)
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

        public static short ToHostOrder(short value, ByteOrder inputByteOrder)
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

        public static long ToHostOrder(long value, ByteOrder inputByteOrder)
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

        public static uint ToHostOrder(uint value, ByteOrder inputByteOrder)
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

        public static ushort ToHostOrder(ushort value, ByteOrder inputByteOrder)
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

        public static ulong ToHostOrder(ulong value, ByteOrder inputByteOrder)
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

        public static float ToHostOrder(float value, ByteOrder inputByteOrder)
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

        public static double ToHostOrder(double value, ByteOrder inputByteOrder)
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

        public static short ToOutputOrder(short value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IPAddress.HostToNetworkOrder(value);
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
        public static int ToOutputOrder(int value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IPAddress.HostToNetworkOrder(value);
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
        public static long ToOutputOrder(long value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IPAddress.HostToNetworkOrder(value);
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
        public static float ToOutputOrder(float value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return IntegerToFloat(IPAddress.HostToNetworkOrder(FloatToInteger(value)));
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
        public static double ToOutputOrder(double value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return LongToDouble(IPAddress.HostToNetworkOrder(DoubleToLong(value)));
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }

        public static ushort ToOutputOrder(ushort value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return SignedToUnsignedShort(IPAddress.HostToNetworkOrder(UnsignedToSignedShort(value)));
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
        public static uint ToOutputOrder(uint value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return SignedToUnsignedInteger(IPAddress.HostToNetworkOrder(UnsignedToSignedInteger(value)));
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
        public static ulong ToOutputOrder(ulong value, ByteOrder outputByteOrder)
        {
            switch (outputByteOrder)
            {
                case ByteOrder.Host:
                    return value;
                case ByteOrder.Network:
                    return SignedToUnsignedLong(IPAddress.HostToNetworkOrder(UnsignedToSignedLong(value)));
                default:
                    throw new ArgumentOutOfRangeException("outputByteOrder");
            }
        }
    }
}
