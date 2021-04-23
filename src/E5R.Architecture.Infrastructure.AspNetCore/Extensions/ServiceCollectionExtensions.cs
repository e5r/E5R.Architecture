// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSetting<TSetting>(
            this IServiceCollection services, IConfiguration config, string key)
            where TSetting : class, new() =>
            ConfigureScopedSetting<TSetting>(services, config, key);
        
        public static IServiceCollection ConfigureScopedSetting<TSetting>(
            this IServiceCollection services, IConfiguration config, string key)
            where TSetting : class, new() => services.AddScoped(_ =>
            Configure<TSetting>(services, config, key) ?? new TSetting());

        public static IServiceCollection ConfigureTransientSetting<TSetting>(
            this IServiceCollection services, IConfiguration config, string key)
            where TSetting : class, new() => services.AddTransient(_ =>
            Configure<TSetting>(services, config, key) ?? new TSetting());

        public static IServiceCollection ConfigureSingletonSetting<TSetting>(
            this IServiceCollection services, IConfiguration config, string key)
            where TSetting : class, new() => services.AddSingleton(_ =>
            Configure<TSetting>(services, config, key) ?? new TSetting());

        private static TSetting Configure<TSetting>(IServiceCollection services, IConfiguration config, string key)
            where TSetting : class, new()
        {
            var section = config.GetSection(key);

            services.Configure<TSetting>(section);

            return section.Get<TSetting>();
        }
    }
}
