using System.Text;
using Construct.Tests.Constructs;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class StringElementTests
    {
        [Test]
        public void CreatesCorrectStringFromBuffer()
        {
            var stream = new ConstructReaderStream(DataStream.Create("test", Encoding.UTF8));

            var planner = new ConstructPlanner();

            var plan = planner.CreatePlan<FixedLengthStringConstruct>();

            var result = plan.ReadConstruct(stream);

            Assert.AreEqual("test", result.FixedString);
        }
    }
}
