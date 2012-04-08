using System.Collections.Generic;
using System.IO;

namespace Construct
{
    public class ConstructPlan<TConstructable> where TConstructable : new()
    {
        private readonly List<PlanAction<TConstructable>> _planActions;

        public ConstructPlan(List<PlanAction<TConstructable>> planActions)
        {
            planActions.RequireNotNull("planActions");
            typeof(TConstructable).Require("TConstructable", argType => argType.IsConstructable());

            _planActions = planActions;
        }

        public TConstructable Parse(ConstructReaderStream inputStream)
        {
            inputStream.RequireNotNull("inputStream");

            var construct = new TConstructable();
            _planActions.ForEach(action => action.ApplyAction(construct, inputStream));
            return construct;
        }
    }
}
