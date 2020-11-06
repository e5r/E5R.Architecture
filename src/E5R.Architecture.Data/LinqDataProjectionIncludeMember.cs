// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace E5R.Architecture.Data
{
    public class LinqDataProjectionIncludeMember
    {
        readonly IList<MemberInfo> _memberChain;

        public LinqDataProjectionIncludeMember(MemberInfo member)
        {
            _memberChain = new List<MemberInfo> { member };
        }

        public LinqDataProjectionIncludeMember(IList<MemberInfo> memberChain)
        {
            _memberChain = memberChain;
        }

        public LinqDataProjectionIncludeMember Append(MemberInfo member)
        {
            _memberChain.Add(member);

            return new LinqDataProjectionIncludeMember(_memberChain);
        }

        public override string ToString()
        {
            var result = _memberChain.Any()
                ? string.Join(".", _memberChain.Select(s => s.Name))
                : default;

            return result;
        }
    }
}
