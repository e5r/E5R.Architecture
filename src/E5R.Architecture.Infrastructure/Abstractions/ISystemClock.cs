// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;

namespace E5R.Architecture.Infrastructure.Abstractions
{
    /// <summary>
    /// The system clock
    /// </summary>
    public interface ISystemClock
    {
        /// <summary>
        /// Get current system time zone information
        /// </summary>
        /// <returns></returns>
        TimeZoneInfo GetCurrentTimeZone();
        
        /// <summary>
        /// Get current date and time
        /// </summary>
        /// <returns></returns>
        DateTimeOffset GetNow();

        /// <summary>
        /// Get current date and time on specific time zone
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        DateTimeOffset GetNow(TimeZoneInfo timeZoneInfo);
        
        /// <summary>
        /// Get current UTC date and time
        /// </summary>
        /// <returns></returns>
        DateTimeOffset GetNowUtc();
        
        /// <summary>
        /// Get current date only
        /// </summary>
        /// <returns></returns>
        DateTimeOffset GetToday();

        /// <summary>
        /// Get current date only on specific time zone
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        DateTimeOffset GetToday(TimeZoneInfo timeZoneInfo);
        
        /// <summary>
        /// Get current UTC date only
        /// </summary>
        /// <returns></returns>
        DateTimeOffset GetTodayUtc();
        
        /// <summary>
        /// Get current time only
        /// </summary>
        /// <returns></returns>
        TimeSpan GetTimeNow();

        /// <summary>
        /// Get current time only on specific time zone
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        TimeSpan GetTimeNow(TimeZoneInfo timeZoneInfo);

        /// <summary>
        /// Get current UTC time only
        /// </summary>
        /// <returns></returns>
        TimeSpan GetTimeNowUtc();
    }
}
