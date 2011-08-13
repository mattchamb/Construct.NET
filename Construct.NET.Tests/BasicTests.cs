using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Construct.NET.Tests
{
    [TestFixture]
    class BasicTests
    {
        [Test]
        public void FactoryTest()
        {
            var construct = ConstructFactory.CreateConstruct<TestConstruct>();
            Assert.IsNotNull(construct);
        }

    }
}
