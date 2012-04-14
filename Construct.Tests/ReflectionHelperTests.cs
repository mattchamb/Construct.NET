using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Construct.Attributes;
using NUnit.Framework;

namespace Construct.Tests
{
    [TestFixture]
    public class ReflectionHelperTests
    {

        [Constructable]
        public class Constructable
        {
            [PrimitiveElement(1)]
            public int Property { get; set; }
        }

        public class NotConstructable
        {
            public int Property { get; set; }
        }

        [Test]
        [TestCase(typeof(Constructable), true)]
        [TestCase(typeof(NotConstructable), false)]
        public void CorrectlyRecognizesConstructableTypes(Type type, bool expected)
        {
            Assert.AreEqual(expected, type.IsConstructable());
        }
    }
}
