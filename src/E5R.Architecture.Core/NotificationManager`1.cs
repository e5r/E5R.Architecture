// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using E5R.Architecture.Core.Extensions;
using static E5R.Architecture.Core.MetaTagAttribute;

namespace E5R.Architecture.Core
{
    public class NotificationManager<TEnum>
        where TEnum : Enum
    {
        private readonly IRuleSet<NotificationMessage<TEnum>> _ruleSet;
        private readonly IEnumerable<INotificationDispatcher<TEnum>> _dispatchers;

        public NotificationManager(
            IRuleSet<NotificationMessage<TEnum>> ruleSet,
            IEnumerable<INotificationDispatcher<TEnum>> dispatchers
        )
        {
            Checker.NotNullArgument(ruleSet, nameof(ruleSet));
            Checker.NotNullArgument(dispatchers, nameof(dispatchers));

            _ruleSet = ruleSet;
            _dispatchers = dispatchers;
        }

        public void Notify(TEnum type, object body, Parameters parameters = null)
            => NotifyAsync(type, body, parameters).GetAwaiter().GetResult();

        public async Task NotifyAsync(TEnum type, object body, Parameters parameters = null)
        {
            Checker.NotNullArgument(type, nameof(type));

            parameters = parameters ?? new Parameters();

            var message = new NotificationMessage<TEnum>(type, body, parameters);
            var ruleCode = type.GetTag(CustomIdKey) ?? type.ToString();

            // TODO: O que fazer com exceptionMessageTemplate aqui?
            await _ruleSet.ByCode(ruleCode).EnsureAsync(message);

            foreach (var dispatcher in _dispatchers) await dispatcher.DispatchAsync(message);
        }
    }
}
