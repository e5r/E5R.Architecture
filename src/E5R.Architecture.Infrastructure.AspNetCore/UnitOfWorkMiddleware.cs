// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace E5R.Architecture.Infrastructure.AspNetCore
{
    using Abstractions;

    public class UnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        public UnitOfWorkMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IUnitOfWork uow)
        {
            await _next(context);

            uow.SaveWork();
        }
    }
}
