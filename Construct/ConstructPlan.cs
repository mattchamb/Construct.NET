using System.Collections.Generic;

namespace Construct
{
    public class ConstructPlan<TConstructable> where TConstructable : new()
    {
        private readonly IList<PlanAction<TConstructable>> _planActions;
        private readonly IConstructPlanner _constructPlanner;

        public ConstructPlan(IList<PlanAction<TConstructable>> planActions, IConstructPlanner constructPlanner)
        {
            Require.NotNull(planActions, "planActions");
            Require.NotNull(constructPlanner, "constructPlanner");
            Require.That(typeof(TConstructable), "TConstructable", typeof(TConstructable).IsConstructable());

            _planActions = planActions;
            _constructPlanner = constructPlanner;
        }

        public int PlanLength { get { return _planActions.Count; } }

        public TConstructable ReadConstruct(ConstructReaderStream inputStream)
        {
            Require.NotNull(inputStream, "inputStream");

            var construct = new TConstructable();

            foreach (var planAction in _planActions)
            {
                planAction.ApplyReadAction(construct, inputStream, _constructPlanner);
            }
            return construct;
        }

        public void WriteConstruct(TConstructable construct, ConstructWriterStream constructWriter)
        {
            foreach (var planAction in _planActions)
            {
                planAction.ApplyWriteAction(construct, constructWriter, _constructPlanner);
            }
        }
    }
}
