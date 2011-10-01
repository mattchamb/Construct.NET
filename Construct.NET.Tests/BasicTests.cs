using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Construct.NET.Tests
{
    [TestFixture]
    class BasicTests
    {
        
        [Test]
        public void IntTest()
        {
            byte[] data = {0xff, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff};
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestIntConstruct>(memStream);

                const int expectedFirst = 255;
                const int expectedSecond = -1;

                Assert.AreEqual(expectedFirst, result.First);
                Assert.AreEqual(expectedSecond, result.Second);
            }
        }

        [Test]
        public void DoubleTest()
        {
            byte[] data = { 0x40, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            data = data.Reverse().ToArray(); //converting it to the correct endian
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestDoubleConstruct>(memStream);

                const double expectedValue = 3.5d;

                Assert.AreEqual(expectedValue, result.Value);
            }
        }

        [Test]
        public void StringTest()
        {
            byte[] data = { (byte)'t', (byte)'e', (byte)'s', (byte)'t', 0 };
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestStringConstruct>(memStream);

                const string expectedValue = "test";

                Assert.AreEqual(expectedValue, result.Value);
            }
        }

        [Test]
        public void NestedTest()
        {
            byte[] data = { 0xff, 0x00, 0x00, 0x00, 0xff, 0x00, 0x00, 0x00, 0xff, 0x00, 0x00, 0x00 };
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestNestedConstruct>(memStream);

                const int expectedValue = 255;

                Assert.AreEqual(expectedValue, result.First);
                Assert.AreEqual(expectedValue, result.Nested.First);
                Assert.AreEqual(expectedValue, result.Nested.Second);
            }
        }

        [Test]
        public void OutputIntTest()
        {

            byte[] expectedData = { 0xff, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff };

            var obj = new TestIntConstruct
                          {
                              First = 255,
                              Second = -1
                          };

            var outStream = Construct.Output<TestIntConstruct>(obj);
            outStream.Seek(0, SeekOrigin.Begin);
            var outData = new byte[8];
            outStream.Read(outData, 0, outData.Length);

            Assert.IsTrue(expectedData.SequenceEqual(outData));
        }

        [Test]
        public void OutputStringTest()
        {
            byte[] expectedData = { (byte)'t', (byte)'e', (byte)'s', (byte)'t', 0 };

            var obj = new TestStringConstruct
                          {
                              Value = "test"
                          };

            var outStream = Construct.Output<TestStringConstruct>(obj);
            outStream.Seek(0, SeekOrigin.Begin);
            var outData = new byte[5];
            outStream.Read(outData, 0, outData.Length);

            Assert.IsTrue(expectedData.SequenceEqual(outData));
        }

        [Test]
        public void ArrayTest()
        {
            byte[] data = { 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x4, 0x00, 0x00, 0x00 };
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestArrayConstruct>(memStream);

                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(1, result.Values[0].First);
                Assert.AreEqual(2, result.Values[0].Second);
                Assert.AreEqual(3, result.Values[1].First);
                Assert.AreEqual(4, result.Values[1].Second);
            }
        }

        [Test]
        public void EnumTest()
        {
            byte[] data = { 0x03, 0x00, 0x00, 0x00 };
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestEnumConstruct>(memStream);
                TestEnum test = TestEnum.Test | TestEnum.Test2;
                //Assert.AreEqual(TestEnum.Test, result.Value);
                Assert.IsTrue(result.Value.HasFlag(TestEnum.Test));
                Assert.IsTrue(result.Value.HasFlag(TestEnum.Test2));
                Assert.IsFalse(result.Value.HasFlag(TestEnum.Test3));
            }
        }

    }
}
