// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogDataModel : DataModel<BlogModel>
    {
        public override object[] IdentifierValues
            => new object[] {Business.BlogUrl};

        public string BlogUrl
        {
            get => Business.BlogUrl;
            set => Business.BlogUrl = value;
        }

        public string BlogTitle
        {
            get => Business.BlogTitle;
            set => Business.BlogTitle = value;
        }
    }
}
