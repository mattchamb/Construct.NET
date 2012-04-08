using System.Collections.Generic;

namespace Construct
{
    public class ConstructPlanner<TConstructable> : IConstructPlanner<TConstructable> where TConstructable : new()
    {
        public ConstructPlan<TConstructable> CreatePlan()
        {
            var planActions = CreatePlanActionsForType();
            return new ConstructPlan<TConstructable>(planActions);
        }

        private List<PlanAction<TConstructable>> CreatePlanActionsForType()
        {
            var constructType = typeof (TConstructable);

            return new List<PlanAction<TConstructable>>();
        }
    }
}