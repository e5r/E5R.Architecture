using System.Threading.Tasks;
// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Core
{
    public class NotificationManager<TEnum>
        where TEnum : Enum
    {
        private readonly RuleFor<NotificationMessage<TEnum>> _validationRule;
        private readonly INotificationDispatcher<TEnum> _dispatcher;

        public NotificationManager(
            RuleFor<NotificationMessage<TEnum>> validationRule,
            INotificationDispatcher<TEnum> dispatcher)
        {
            Checker.NotNullArgument(validationRule, nameof(validationRule));
            Checker.NotNullArgument(dispatcher, nameof(dispatcher));

            _validationRule = validationRule;
            _dispatcher = dispatcher;
        }

        public void Notify(TEnum type, object body, Parameters parameters = null)
            => NotifyAsync(type, body, parameters).Wait();

        public async Task NotifyAsync(TEnum type, object body, Parameters parameters = null)
        {
            Checker.NotNullArgument(type, nameof(type));

            parameters = parameters ?? new Parameters();

            var message = new NotificationMessage<TEnum>(type, body, parameters);

            await _validationRule.EnsureAsync(message);
            await _dispatcher.DispatchAsync(message);
        }
    }
}
