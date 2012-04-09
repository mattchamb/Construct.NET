using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Construct.Tests
{
    /// <summary>
    /// These tests check that my <see cref="ByteOrderer"/> does infact swap byte orders depending on the architecture of the system.
    /// </summary>
    [TestFixture]
    public class ByteOrdererTests
    {
        /// <summary>
        /// Network byte order is BigEndian
        /// </summary>
        private readonly bool _hostIsNetworkByteOrder = !BitConverter.IsLittleEndian;


        [Test]
        [TestCase(123, 76)]
        [TestCase(byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue)]
        public void ConvertingShortFromHostOrderToHostOrderShouldNotChangeValue(byte b1, byte b2)
        {
            var data = new[] { b1, b2 };
            var value = BitConverter.ToInt16(data, 0);
            var result = ByteOrderer.ToHostOrder(value, ByteOrder.Host);
            Assert.AreEqual(value, result);
        }

        [Test]
        [TestCase(123, 76)]
        [TestCase(byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue)]
        public void ConvertingShortFromNetworkOrderToHostOrderShouldBeCorrectDependingOnSystemArchitecture(byte b1, byte b2)
        {
            var data = new[] { b1, b2 };
            byte[] expected = _hostIsNetworkByteOrder ? data : new [] { b2, b1 };

            var value = BitConverter.ToInt16(data, 0);
            var convertedValue = ByteOrderer.ToHostOrder(value, ByteOrder.Network);
            var result = BitConverter.GetBytes(convertedValue);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(123, 76)]
        [TestCase(byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue)]
        public void ConvertingUShortFromHostOrderToHostOrderShouldNotChangeValue(byte b1, byte b2)
        {
            var data = new[] { b1, b2 };
            var value = BitConverter.ToUInt16(data, 0);
            var result = ByteOrderer.ToHostOrder(value, ByteOrder.Host);
            Assert.AreEqual(value, result);
        }

        [Test]
        [TestCase(123, 76)]
        [TestCase(byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue)]
        public void ConvertingUShortFromNetworkOrderToHostOrderShouldBeCorrectDependingOnSystemArchitecture(byte b1, byte b2)
        {
            var data = new[] { b1, b2 };
            byte[] expected = _hostIsNetworkByteOrder ? data : new [] { b2, b1 };

            var value = BitConverter.ToUInt16(data, 0);
            var convertedValue = ByteOrderer.ToHostOrder(value, ByteOrder.Network);
            var result = BitConverter.GetBytes(convertedValue);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(123, 76, 32, 0)]
        [TestCase(byte.MaxValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue, byte.MinValue, byte.MinValue)]
        [TestCase(byte.MinValue, byte.MaxValue, byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        public void ConvertingIntegerFromHostOrderToHostOrderShouldNotChangeValue(byte b1, byte b2, byte b3, byte b4)
        {
            var data = new[] { b1, b2, b3, b4 };
            var value = BitConverter.ToInt32(data, 0);
            var result = ByteOrderer.ToHostOrder(value, ByteOrder.Host);
            Assert.AreEqual(value, result);
        }

        [Test]
        [TestCase(123, 76, 32, 0)]
        [TestCase(byte.MaxValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue, byte.MinValue, byte.MinValue)]
        [TestCase(byte.MinValue, byte.MaxValue, byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        public void ConvertingIntegerFromNetworkOrderToHostOrderShouldBeCorrectDependingOnSystemArchitecture(byte b1, byte b2, byte b3, byte b4)
        {
            var data = new[] { b1, b2, b3, b4 };
            byte[] expected = _hostIsNetworkByteOrder ? data : new [] { b4, b3, b2, b1 };

            var value = BitConverter.ToInt32(data, 0);
            var convertedValue = ByteOrderer.ToHostOrder(value, ByteOrder.Network);
            var result = BitConverter.GetBytes(convertedValue);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(123, 76, 32, 0)]
        [TestCase(byte.MaxValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue, byte.MinValue, byte.MinValue)]
        [TestCase(byte.MinValue, byte.MaxValue, byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        public void ConvertingFloatFromHostOrderToHostOrderShouldNotChangeValue(byte b1, byte b2, byte b3, byte b4)
        {
            var data = new[] { b1, b2, b3, b4 };
            var value = BitConverter.ToSingle(data, 0);
            var result = ByteOrderer.ToHostOrder(value, ByteOrder.Host);
            Assert.AreEqual(value, result);
        }

        [Test]
        [TestCase(123, 76, 32, 0)]
        [TestCase(byte.MaxValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        [TestCase(byte.MaxValue, byte.MaxValue, byte.MinValue, byte.MinValue)]
        [TestCase(byte.MinValue, byte.MaxValue, byte.MinValue, byte.MaxValue)]
        [TestCase(byte.MinValue, byte.MinValue, byte.MaxValue, byte.MinValue)]
        public void ConvertingFloatFromNetworkOrderToHostOrderShouldBeCorrectDependingOnSystemArchitecture(byte b1, byte b2, byte b3, byte b4)
        {
            var data = new[] { b1, b2, b3, b4 };
            byte[] expected = _hostIsNetworkByteOrder ? data : new[] { b4, b3, b2, b1 };

            var value = BitConverter.ToSingle(data, 0);
            var convertedValue = ByteOrderer.ToHostOrder(value, ByteOrder.Network);
            var result = BitConverter.GetBytes(convertedValue);

            Assert.AreEqual(expected, result);
        }
    }
}
