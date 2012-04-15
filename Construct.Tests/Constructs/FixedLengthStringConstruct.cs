using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Construct.Attributes;

namespace Construct.Tests.Constructs
{
    [Constructable]
    public class FixedLengthStringConstruct
    {
        [FixedLengthStringElement(1, 4, "UTF-8")]
        public string FixedString { get; set; }
    }
}
