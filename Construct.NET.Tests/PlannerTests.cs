using NUnit.Framework;
using Construct.NET;

namespace Construct.NET.Tests
{
    [TestFixture]
    class PlannerTests
    {
        private IConstructPlanner _planner;

        [TestFixtureSetUp]
        public void SetupPlannerTests()
        {
            _planner = new ConstructPlanner();
        }

        [Construct]
        class IntConstruct
        {
            [ConstructField(1)]
            public int First { get; set; }

            [ConstructField(2)]
            public int Second { get; set; }
        }

        [Test]
        public void TestPlannerIntConstruct()
        {
            var plan = _planner.CreateConstructPlan<IntConstruct>();
            Assert.AreEqual(2, plan.PlanActions.Count);
            Assert.IsTrue(plan.PlanActions[0] is Int32Action);
            Assert.IsTrue(plan.PlanActions[1] is Int32Action);
        }

        [Construct]
        class DoubleConstruct
        {
            [ConstructField(1)]
            public double D { get; set; } 
        }

        [Test]
        public void TestPlannerDoubleConstruct()
        {
            var plan = _planner.CreateConstructPlan<DoubleConstruct>();
            Assert.AreEqual(1, plan.PlanActions.Count);
            Assert.IsTrue(plan.PlanActions[0] is DoubleAction);
        }

        [Construct]
        class NestedConstruct
        {
            [ConstructField(1)]
            public DoubleConstruct Dc { get; set; }
        }

        [Test]
        public void TestPlannerNestedConstruct()
        {
            var plan = _planner.CreateConstructPlan<NestedConstruct>();
            Assert.AreEqual(1, plan.PlanActions.Count);
            Assert.IsTrue(plan.PlanActions[0] is NestedAction);
        }

        [Construct]
        class ArrayConstruct
        {
            [ConstructField(1)]
            public int ArrayLength { get; set; }
            [ConstructField(2)]
            public int[] Array { get; set; }
        }

        [Test]
        public void TestPlannerArrayConstruct()
        {
            var plan = _planner.CreateConstructPlan<ArrayConstruct>();
            Assert.AreEqual(2, plan.PlanActions.Count);
            Assert.IsTrue(plan.PlanActions[0] is Int32Action);
            Assert.IsTrue(plan.PlanActions[1] is ArrayAction);
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
        public void TestPlannerConditionalConstruct()
        {
            var plan = _planner.CreateConstructPlan<ConditionalConstruct>();
            Assert.AreEqual(2, plan.PlanActions.Count);
            Assert.IsTrue(plan.PlanActions[0] is Int32Action);
            Assert.IsTrue(plan.PlanActions[1] is Int32Action);
        }
    }
}
