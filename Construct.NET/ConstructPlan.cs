using System.Collections.Generic;

namespace Construct.NET
{
    internal class ConstructPlan
    {
        public ConstructPlan()
        {
            PlanActions = new List<ConstructPlanAction>();
        }

        public IList<ConstructPlanAction> PlanActions { get; private set; }
    }
}