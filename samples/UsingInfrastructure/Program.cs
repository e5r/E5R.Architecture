﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static System.Diagnostics.Debug;

namespace UsingInfrastructure
{
    public class MySettings
    {
        public const string Key = "My";
        
        public string Message { get; set; }

        public MySettings()
        {
            Message = "Default message";
        }
    }

    public interface IMyModel2Fail
    {
        RuleCheckResult GetFail();
    }

    public class MyModel2Fail : IMyModel2Fail
    {
        public RuleCheckResult GetFail()
        {
            return RuleCheckResult.Fail;
        }
    }

    public class MyModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

    public class NameIsBrazilRule : RuleFor<MyModel>
    {
        public NameIsBrazilRule() : base("RN-001", "Name must be 'Brazil'")
        {
        }

        public override async Task<RuleCheckResult> CheckAsync(MyModel target)
        {
            if (target?.Name?.ToLowerInvariant() != "brazil")
                return await Fail();

            return await Success();
        }
    }

    public class NumberMinimalRule : RuleFor<MyModel>
    {
        public NumberMinimalRule() : base("RN-002", "Number must be greater than 10")
        {
        }

        public override async Task<RuleCheckResult> CheckAsync(MyModel target)
        {
            if (target?.Number > 10)
                return await Success();

            return await Fail();
        }
    }

    public class NameIsExactlyBrazilRule : RuleFor<MyModel>
    {
        public NameIsExactlyBrazilRule()
            : base("RN-003", "critical", "Name must be exactly 'Brazil'")
        {
        }

        public override async Task<RuleCheckResult> CheckAsync(MyModel target)
        {
            if (target?.Name != "Brazil")
                return await Fail();

            return await Success();
        }
    }

    public class NumberIsBigRule : RuleFor<MyModel>
    {
        private readonly IMyModel2Fail _fail;

        public NumberIsBigRule(IMyModel2Fail fail)
            : base("RN-004", "critical", "Number must be between 35 and 80")
        {
            // Permite injeção de dependências
            _fail = fail;
        }

        public override async Task<RuleCheckResult> CheckAsync(MyModel target)
        {
            if (target?.Number >= 35 && target.Number <= 80)
                return await Success();

            return _fail.GetFail();
        }
    }

    public class TransformSourceData
    {
        public DateTime SourceTime { get; set; }
        public int WordCount { get; set; }
        public string Word { get; set; }
    }

    public class TransformDestinationData
    {
        public string[] TimeWords { get; set; }
    }

    public class
        TransformSourceToDestinationData : ITransformer<TransformSourceData,
            TransformDestinationData>
    {
        public TransformDestinationData Transform(TransformSourceData @from)
        {
            return new TransformDestinationData
            {
                TimeWords = Enumerable.Range(1, @from.WordCount)
                    .Select(w =>
                        $"{@from.Word} {w} [{@from.SourceTime.ToShortDateString()} - {@from.SourceTime.ToShortTimeString()}]")
                    .ToArray()
            };
        }
    }

    public class Program
    {
        public Program(IOptions<MySettings> optionsSettings, MySettings settings, IRuleSet<MyModel> ruleset, ITransformationManager transformer)
        {
            // Data transformations
            TransformSourceData sourceData = new TransformSourceData()
            {
                Word = "Development",
                SourceTime = DateTime.Now,
                WordCount = 100
            };

            TransformDestinationData destinationData =
                transformer.Transform<TransformSourceData, TransformDestinationData>(sourceData);

            foreach (var timeWord in destinationData.TimeWords)
            {
                Console.WriteLine("Time word: {0}", timeWord);
            }

            // Rule validations
            var model1 = new MyModel();
            var model2 = new MyModel
            {
                Name = "brazil",
                Number = 34
            };

            var result1 = ruleset.Check(model1);
            var result2 = ruleset.ByDefaultCategory().Check(model1);
            var result3 = ruleset.ByCategory("critical").Check(model1);
            var result4 = ruleset.ByCode("RN-001").Check(model1);
            var result5 = ruleset.ByCode(new[] {"RN-002", "RN-004"}).Check(model1);
            var result6 = ruleset.ByDefaultCategory().Check(model2);
            var result7 = ruleset.ByCategory("critical").Check(model2);

            Assert(result1 != null, "result != null");
            Assert(!result1.IsSuccess, "!result.IsSuccess");
            Assert(result1.Unconformities != null, "result.Unconformities != null");
            Assert(result1.Unconformities.Count == 4, "result.Unconformities.Count == 4");
            Assert(string.Join(" ", result2.Unconformities.Keys) == "RN-001 RN-002",
                "RN-001 RN-002");
            Assert(string.Join(" ", result3.Unconformities.Keys) == "RN-003 RN-004",
                "RN-003 RN-004");
            Assert(string.Join(" ", result4.Unconformities.Keys) == "RN-001", "RN-001");
            Assert(string.Join(" ", result5.Unconformities.Keys) == "RN-002 RN-004",
                "RN-002 RN-004");
            Assert(result6.IsSuccess, "result6.IsSuccess");
            Assert(!result7.IsSuccess, "!result7.IsSuccess");
            Assert(string.Join(" ", result7.Unconformities.Keys) == "RN-003 RN-004",
                "RN-003 RN-004 (result7)");
        }

        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddInMemoryCollection(new[]
                        {
                            KeyValuePair.Create<string, string>("My:Message", "Minha mensagem"), 
                        })
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((hostBuilder, s) => s.AddInfrastructure(hostBuilder.Configuration))
                .Build();
            
            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<Program>();
            }
        }
    }

    public class CrossCuttingRegistrar : ICrossCuttingRegistrar
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Program>();
            services.AddScoped<IMyModel2Fail, MyModel2Fail>();

            services.AddSettings<MySettings>(ServiceLifetime.Scoped, configuration, MySettings.Key);
            services.AddTransientSettings<MySettings>(configuration, MySettings.Key);
            services.AddScopedSettings<MySettings>(configuration, MySettings.Key);
            services.AddSingletonSettings<MySettings>(configuration, MySettings.Key);
        }
    }
}
