namespace Construct.NET
{
    public static class ConstructFactory
    {
        public static IConstruct<T> CreateConstruct<T>()
        {
            
            IConstructPlanner planner = new ConstructPlanner();
            IConstructParser parser = new ConstructParser();
            IConstructOutputter outputter = new ConstructOutputter();

            IConstruct<T> construct = new Construct<T>(planner, parser, outputter);

            return construct;
        }
    }
}