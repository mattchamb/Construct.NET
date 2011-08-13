using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Construct.NET.Tests
{
    [TestFixture]
    class BasicTests
    {

        [Test]
        public void FactoryTest()
        {
            var construct = ConstructFactory.CreateConstruct<TestIntConstruct>();
            Assert.IsNotNull(construct);
        }
        
        [Test]
        public void IntTest()
        {
            var construct = ConstructFactory.CreateConstruct<TestIntConstruct>();
            byte[] data = {0xff, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff};
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = construct.Parse(memStream);

                int expectedFirst = 255;
                int expectedSecond = -1;

                Assert.AreEqual(expectedFirst, result.First);
                Assert.AreEqual(expectedSecond, result.Second);
            }
        }

        [Test]
        public void DoubleTest()
        {
            var construct = ConstructFactory.CreateConstruct<TestDoubleConstruct>();
            byte[] data = { 0x40, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            data = data.Reverse().ToArray(); //converting it to the correct endian
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = construct.Parse(memStream);

                double expectedValue = 3.5d;

                Assert.AreEqual(expectedValue, result.Value);
            }
        }

        [Test]
        public void StringTest()
        {
            var construct = ConstructFactory.CreateConstruct<TestStringConstruct>();
            byte[] data = { (byte)'t', (byte)'e', (byte)'s', (byte)'t', 0 };
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = construct.Parse(memStream);

                var expectedValue = "test";

                Assert.AreEqual(expectedValue, result.Value);
            }
        }
    }
}
