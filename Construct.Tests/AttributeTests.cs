using System.Linq;
using Construct.Tests.Constructs;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class AttributeTests
    {
        [Test]
        public void CheckGenericTypesWorkAsExpected()
        {
            var properties = typeof (SingleIntConstruct).GetProperties();
            var attributes = properties.SelectMany(x => x.GetCustomAttributes(typeof(ConstructElementDescriptor), false)).Cast<ConstructElementDescriptor>().ToList();
        }
    }
}
