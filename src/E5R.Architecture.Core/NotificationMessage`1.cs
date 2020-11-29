// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    public class NotificationMessage<TEnum>
        where TEnum : Enum
    {
        public NotificationMessage(TEnum type, object body, Parameters parameters)
        {
            Type = type;
            Body = body;
            Parameters = parameters;
        }

        public TEnum Type { get; }
        public object Body { get; }
        public Parameters Parameters { get; }
    }
}
