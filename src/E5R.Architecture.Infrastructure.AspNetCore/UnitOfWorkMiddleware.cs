// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace E5R.Architecture.Infrastructure.AspNetCore
{
    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnitOfWorkMiddleware> _logger;

        public UnitOfWorkMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            Checker.NotNullArgument(next, nameof(next));
            Checker.NotNullArgument(loggerFactory, nameof(loggerFactory));

            _next = next;
            _logger = loggerFactory.CreateLogger<UnitOfWorkMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork uow)
        {
            Checker.NotNullArgument(context, nameof(context));
            Checker.NotNullArgument(uow, nameof(uow));

            _logger.LogTrace("Request start with transaction control");

            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                _logger.LogTrace("Discarding unit of work");
                uow.DiscardWork();
                _logger.LogTrace("Unit of work successfully discarded");

                throw;
            }

            _logger.LogTrace("Saving unit of work");
            uow.SaveWork();
            _logger.LogTrace("Unit of work successfully saved");
        }
    }
}
