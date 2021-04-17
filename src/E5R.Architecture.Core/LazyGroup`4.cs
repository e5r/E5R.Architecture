// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    public class LazyGroup<TItem1, TItem2, TItem3, TItem4>
        where TItem1 : class
        where TItem2 : class
        where TItem3 : class
        where TItem4 : class
    {
        private readonly ILazy<TItem1> _item1;
        private readonly ILazy<TItem2> _item2;
        private readonly ILazy<TItem3> _item3;
        private readonly ILazy<TItem4> _item4;

        public LazyGroup(ILazy<TItem1> item1, ILazy<TItem2> item2, ILazy<TItem3> item3,
            ILazy<TItem4> item4)
        {
            Checker.NotNullArgument(item1, nameof(item1));
            Checker.NotNullArgument(item2, nameof(item2));
            Checker.NotNullArgument(item3, nameof(item3));
            Checker.NotNullArgument(item4, nameof(item4));

            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
            _item4 = item4;
        }

        protected TItem1 Item1 => _item1.Value;
        protected TItem2 Item2 => _item2.Value;
        protected TItem3 Item3 => _item3.Value;
        protected TItem4 Item4 => _item4.Value;
    }
}
