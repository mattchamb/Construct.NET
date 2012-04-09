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
            var typeActionResolver = new DefaultTypeActionResolver();
            _constructPlanner = new ConstructPlanner(typeActionResolver);
        }

        [Test]
        public void CreatePlanForSingleIntegerConstruct()
        {
            var plan = _constructPlanner.CreatePlan<SingleIntConstruct>();

            Assert.AreEqual(1, plan.PlanLength);
        }

        [Test]
        public void PlanForSingleIntConstructExecutesWithCorrectResult()
        {
            var plan = _constructPlanner.CreatePlan<SingleIntConstruct>();

            var reader = new ConstructReaderStream(DataStream.Create(555, ByteOrder.Host));

            var result = plan.ReadConstruct(reader);

            Assert.AreEqual(555, result.Integer);
        }
    }
}
