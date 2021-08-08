// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    public class LazyTuple<TItem1>
        where TItem1 : class
    {
        private readonly ILazy<TItem1> _item1;

        public LazyTuple(ILazy<TItem1> item1)
        {
            Checker.NotNullArgument(item1, nameof(item1));

            _item1 = item1;
        }

        public TItem1 Item1 => _item1.Value;
    }
}
