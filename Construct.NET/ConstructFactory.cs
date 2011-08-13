namespace Construct.NET
{
    public static class ConstructFactory
    {
        public static IConstruct<T> CreateConstruct<T>()
        {
            
            IConstructPlanner planner = new ConstructPlanner();
            IConstructParser parser = new ConstructParser();

            IConstruct<T> construct = new Construct<T>(planner, parser);

            return construct;
        }
    }
}