// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;
using System;

namespace E5R.Architecture.Core
{
    public class NotificationManager<TEnum>
        where TEnum : Enum
    {
        private readonly IRuleSet<NotificationMessage<TEnum>> _ruleSet;
        private readonly INotificationDispatcher<TEnum> _dispatcher;

        public NotificationManager(
            IRuleSet<NotificationMessage<TEnum>> ruleSet,
            INotificationDispatcher<TEnum> dispatcher)
        {
            Checker.NotNullArgument(ruleSet, nameof(ruleSet));
            Checker.NotNullArgument(dispatcher, nameof(dispatcher));

            _ruleSet = ruleSet;
            _dispatcher = dispatcher;
        }

        public void Notify(TEnum type, object body, Parameters parameters = null)
            => NotifyAsync(type, body, parameters).Wait();

        public async Task NotifyAsync(TEnum type, object body, Parameters parameters = null)
        {
            Checker.NotNullArgument(type, nameof(type));

            parameters = parameters ?? new Parameters();
            
            var message = new NotificationMessage<TEnum>(type, body, parameters);

            // TODO: O que fazer com exceptionMessageTemplate aqui?
            await _ruleSet.EnsureAsync(message);
            await _dispatcher.DispatchAsync(message);
        }
    }
}
