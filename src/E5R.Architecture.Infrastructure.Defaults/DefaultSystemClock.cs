// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using E5R.Architecture.Infrastructure.Abstractions;

namespace E5R.Architecture.Infrastructure.Defaults
{
    /// <summary>
    /// Default implementation of <see cref="ISystemClock"/>
    /// </summary>
    public class DefaultSystemClock : ISystemClock
    {
        public TimeZoneInfo GetCurrentTimeZone()
        {
            return TimeZoneInfo.Local;
        }

        public DateTimeOffset GetNow()
        {
            return new DateTimeOffset(DateTime.Now);
        }

        public DateTimeOffset GetNow(TimeZoneInfo timeZoneInfo)
        {
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            return new DateTimeOffset(dateTime);
        }

        public DateTimeOffset GetNowUtc()
        {
            return DateTime.UtcNow;
        }

        public DateTimeOffset GetToday()
        {
            var dateTime = DateTime.Now;

            return new DateTimeOffset(dateTime, TimeSpan.Zero);
        }

        public DateTimeOffset GetToday(TimeZoneInfo timeZoneInfo)
        {
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            return new DateTimeOffset(dateTime, TimeSpan.Zero);
        }

        public DateTimeOffset GetTodayUtc()
        {
            var dateTime = DateTime.UtcNow;

            return new DateTimeOffset(dateTime, TimeSpan.Zero);
        }

        public TimeSpan GetTimeNow()
        {
            return DateTime.Now.TimeOfDay;
        }

        public TimeSpan GetTimeNow(TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo).TimeOfDay;
        }

        public TimeSpan GetTimeNowUtc()
        {
            return DateTime.UtcNow.TimeOfDay;
        }
    }
}
