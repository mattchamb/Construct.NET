using System.IO;
using NUnit.Framework;

namespace Construct.NET.Tests
{
    [TestFixture]
    class DynamicTests
    {
        [Test]
        public void BasicTest()
        {
            dynamic ctr = new DynamicConstruct();
            ctr.Value = CtrType.Int32;
            byte[] data = { 0xff, 0x00, 0x00, 0x00 };
            using (var memStream = new MemoryStream(data))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                ctr.Parse(memStream);
                const int expected = 255;

                Assert.AreEqual(expected, ctr.Value);
            }
            
        }
    }
}
