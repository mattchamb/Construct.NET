using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Construct.NET.Tests
{
    [TestFixture]
    class ParserTests
    {
        [Test]
        public void IntTest()
        {
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write(255);
                writer.Write(-1);
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
            
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write(3.5d);
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestDoubleConstruct>(memStream);

                const double expectedValue = 3.5d;

                Assert.AreEqual(expectedValue, result.Value);
            }
        }

        [Test]
        public void StringTest()
        {
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write(Encoding.ASCII.GetBytes("test"));
                writer.Write((byte)0);
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestStringConstruct>(memStream);

                const string expectedValue = "test";

                Assert.AreEqual(expectedValue, result.Value);
            }
        }

        [Test]
        public void NestedTest()
        {
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write(255);
                writer.Write(255);
                writer.Write(255);
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
            var outData =  Construct.Output(obj);
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

            
            var outData = Construct.Output(obj);
            
            Assert.IsTrue(expectedData.SequenceEqual(outData));
        }

        [Test]
        public void ArrayTest()
        {
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write(2);
                writer.Write(1);
                writer.Write(2);
                writer.Write(3);
                writer.Write(4);
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
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write((byte)(TestByteEnum.Flag1 | TestByteEnum.Flag2));
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<TestEnumConstruct>(memStream);
                Assert.IsTrue(result.Value.HasFlag(TestByteEnum.Flag1));
                Assert.IsTrue(result.Value.HasFlag(TestByteEnum.Flag2));
                Assert.IsFalse(result.Value.HasFlag(TestByteEnum.Flag3));
            }
        }

        [Construct]
        class ConditionalConstruct
        {
            [ConstructField(1)]
            public int Prop { get; set; }
            [ConstructField(2, ConditionFunction = "ConditionFunction")]
            public int SomeInt { get; set; }

            public bool ConditionFunction()
            {
                return true;
            }
        }

        [Test]
        public void TestParseConditionalConstruct()
        {
            using (var memStream = new MemoryStream())
            {
                var writer = new BinaryWriter(memStream, Encoding.ASCII);
                writer.Write(255);
                writer.Write(255);
                memStream.Seek(0, SeekOrigin.Begin);
                var result = Construct.Parse<ConditionalConstruct>(memStream);

                const int expectedValue = 255;

                Assert.AreEqual(expectedValue, result.Prop);
                Assert.AreEqual(expectedValue, result.SomeInt);
            }
        }

    }
}
