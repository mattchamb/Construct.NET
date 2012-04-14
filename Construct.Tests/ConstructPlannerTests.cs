using System.IO;
using Construct.Tests.Constructs;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class ConstructPlannerTests
    {
        private readonly IConstructPlanner _constructPlanner;

        public ConstructPlannerTests()
        {
            var typeActionResolver = new DefaultTypeActionResolver(new LambdaGenerator());
            _constructPlanner = new ConstructPlanner(typeActionResolver);
        }

        [Test]
        public void CreatePlanForSingleIntegerConstruct()
        {
            var plan = _constructPlanner.CreatePlan<SingleIntConstruct>();

            Assert.AreEqual(1, plan.PlanLength);
        }

        [Test]
        public void PlanForSingleIntConstructExecutesReadingWithCorrectResult()
        {
            var plan = _constructPlanner.CreatePlan<SingleIntConstruct>();

            var reader = new ConstructReaderStream(DataStream.Create(555, ByteOrder.Host));

            var result = plan.ReadConstruct(reader);

            Assert.AreEqual(555, result.Integer);
        }
        
        [Test]
        public void PlanForSingleIntConstructExecutesWritingWithCorrectResult()
        {
            var plan = _constructPlanner.CreatePlan<SingleIntConstruct>();
            using(var baseStream = new MemoryStream())
            {
                var outStream = new ConstructWriterStream(baseStream);
                var obj = new SingleIntConstruct()
                              {
                                  Integer = 555
                              };

                plan.WriteConstruct(obj, outStream);
                Assert.AreEqual(4, outStream.Length);
            }
        }
    }
}
