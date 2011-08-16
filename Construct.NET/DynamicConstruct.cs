using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Construct.NET
{
    public class DynamicConstruct : DynamicObject
    {
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return base.TrySetMember(binder, value);
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return base.TryInvokeMember(binder, args, out result);
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return base.TryGetMember(binder, out result);
        }
    }
}
