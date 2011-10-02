using System;

namespace Construct.NET.Tests
{
    [Flags]
    public enum TestByteEnum : byte
    {
        Node = 0,
        Flag1 = 1,
        Flag2 = 2,
        Flag3 = 4
    }
}