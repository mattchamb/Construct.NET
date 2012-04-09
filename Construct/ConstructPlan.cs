using System.Collections.Generic;

namespace Construct
{
    public class ConstructPlan<TConstructable> where TConstructable : new()
    {
        private readonly IList<PlanAction<TConstructable>> _planActions;

        public ConstructPlan(IList<PlanAction<TConstructable>> planActions)
        {
            planActions.RequireNotNull("planActions");
            typeof(TConstructable).Require("TConstructable", argType => argType.IsConstructable());

            _planActions = planActions;
        }

        public int PlanLength { get { return _planActions.Count; } }

        public TConstructable ReadConstruct(ConstructReaderStream inputStream)
        {
            inputStream.RequireNotNull("inputStream");

            var construct = new TConstructable();

            foreach (var planAction in _planActions)
            {
                planAction.ApplyReadAction(construct, inputStream);
            }
            return construct;
        }

        public void WriteConstruct(TConstructable construct, ConstructWriterStream constructWriter)
        {
            foreach (var planAction in _planActions)
            {
                planAction.ApplyWriteAction(construct, constructWriter);
            }
        }
    }
}
