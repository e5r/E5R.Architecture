﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    public class LazyTuple<TItem1, TItem2, TItem3>
        where TItem1 : class
        where TItem2 : class
        where TItem3 : class
    {
        private readonly ILazy<TItem1> _item1;
        private readonly ILazy<TItem2> _item2;
        private readonly ILazy<TItem3> _item3;

        public LazyTuple(ILazy<TItem1> item1, ILazy<TItem2> item2, ILazy<TItem3> item3)
        {
            Checker.NotNullArgument(item1, nameof(item1));
            Checker.NotNullArgument(item2, nameof(item2));
            Checker.NotNullArgument(item3, nameof(item3));

            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
        }

        public TItem1 Item1 => _item1.Value;
        public TItem2 Item2 => _item2.Value;
        public TItem3 Item3 => _item3.Value;
    }
}
