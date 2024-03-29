﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E5R.Architecture.Business;
using E5R.Architecture.Business.Extensions;
using E5R.Architecture.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static E5R.Architecture.Core.MetaTagAttribute;

namespace UsingBusiness
{
    /// <summary>
    /// Ação que gera um número aleatório
    /// </summary>
    public class GenerateRandomNumberHandler : OutputOnlyActionHandler<int>
    {
        protected override Task<int> ExecActionAsync() =>
            Task.FromResult(new Random().Next(int.MinValue, int.MaxValue));
    }

    /// <summary>
    /// Ação que processa uma string de entrada
    /// </summary>
    public class ProcessStringHandler : InputOnlyActionHandler<string>
    {
        protected override Task ExecActionAsync(string input)
        {
            // Aqui é garantido que "input" não é nulo

            if (string.IsNullOrEmpty(input))
            {
                input = "abc";
            }

            var builder = new StringBuilder();

            input.ToList().ForEach(c =>
            {
                var bytes = new byte[(int)c];

                new Random().NextBytes(bytes);

                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x"));
                }
            });

            var stringGerada = builder.ToString();
            var tamanhoString = stringGerada.Length;

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Ação que gera uma senha aleatória com base em uma lista de caracteres válidos
    /// </summary>
    public class GenerateRandomPasswordHandler : ActionHandler<(string, int), string>
    {
        protected override Task<string> ExecActionAsync((string, int) input)
        {
            var template = input.Item1;
            var count = input.Item2;

            Checker.NotEmptyOrWhiteArgument(template, "input(string)");

            if (count < 3)
            {
                throw new Exception("O password precisa de pelo menos 3 caracteres");
            }

            var builder = new StringBuilder();
            var rnd = new Random();

            while (builder.Length < count)
            {
                var idx = rnd.Next(0, template.Length - 1);

                builder.Append(template.ElementAt(idx));
            }

            return Task.FromResult(builder.ToString());
        }
    }

    /// <summary>
    /// Ação que executa todas as outras 3 ações
    /// </summary>
    public class ExecAllHandler : ActionHandler
    {
        private readonly GenerateRandomNumberHandler _generateRandomNumberHandler;
        private readonly GenerateRandomPasswordHandler _generateRandomPasswordHandler;
        private readonly ProcessStringHandler _processStringHandler;

        public ExecAllHandler(GenerateRandomNumberHandler generateRandomNumberHandler,
            GenerateRandomPasswordHandler generateRandomPasswordHandler,
            ProcessStringHandler processStringHandler)
        {
            Checker.NotNullArgument(generateRandomNumberHandler,
                nameof(generateRandomNumberHandler));
            Checker.NotNullArgument(generateRandomPasswordHandler,
                nameof(generateRandomPasswordHandler));
            Checker.NotNullArgument(processStringHandler, nameof(processStringHandler));

            _generateRandomNumberHandler = generateRandomNumberHandler;
            _generateRandomPasswordHandler = generateRandomPasswordHandler;
            _processStringHandler = processStringHandler;
        }

        protected override async Task ExecActionAsync()
        {
            var number = await _generateRandomNumberHandler.ExecAsync();
            var password = await _generateRandomPasswordHandler.ExecAsync((
                "012345 6789abcdefgh    ijklmnopqrs tuvwxyz-)(*&ˆˆ$#@!", 10));

            await _processStringHandler.ExecAsync("nova string");
        }
    }

    /// <summary>
    /// Fachada para ações que aceitam parâmetros de entrada
    /// </summary>
    /// <remarks>
    /// Aqui chamamos de serviço de negócio (BusinessService) mas pode ser qualquer coisa que
    /// você preferir: BusinessModule, Service, Module, Facade, etc.
    /// </remarks>
    public class DadosEntradaBusinessService
    {
        private readonly LazyTuple<ProcessStringHandler, GenerateRandomPasswordHandler> _g;

        public DadosEntradaBusinessService(
            LazyTuple<ProcessStringHandler, GenerateRandomPasswordHandler> tuple)
        {
            Checker.NotNullArgument(tuple, nameof(tuple));

            _g = tuple;
        }

        /// <summary>
        /// Processa dados aleatórios com base em uma string de entrada
        /// </summary>
        /// <param name="inputString">String de entrada</param>
        public async Task ProcessString(string inputString) =>
            await _g.Item1.ExecAsync(inputString);

        /// <summary>
        /// Gera uma senha aleatório com base em uma tupla que informa os caracteres possíveis e o tamanho da senha pretendida
        /// </summary>
        /// <param name="input">Tupla (<see cref="Tuple{T1,T2}"/>) de string com os caracteres possíveis, e int com o tamanho da senha pretendida</param>
        public async Task<string> GenerateRandomPassword((string, int) input) =>
            await _g.Item2.ExecAsync(input);
    }

    /// <summary>
    /// Fachada para características de negócio que produzem resultado de saída
    /// </summary>
    /// <remarks>
    /// Aqui chamamos de serviço de negócio (BusinessService) mas pode ser qualquer coisa que
    /// você preferir: BusinessModule, Service, Module, Facade, etc.
    /// </remarks>
    public class DadosSaidaBusinessService
    {
        private readonly LazyTuple<GenerateRandomNumberHandler, ProcessStringHandler> _g;

        public DadosSaidaBusinessService(
            LazyTuple<GenerateRandomNumberHandler, ProcessStringHandler> tuple)
        {
            Checker.NotNullArgument(tuple, nameof(tuple));

            _g = tuple;
        }

        /// <summary>
        /// Gera um número aleatório
        /// </summary>
        public async Task<int> GenerateRandomNumber() => await _g.Item1.ExecAsync();

        /// <summary>
        /// Processa dados aleatórios com base em uma string de entrada
        /// </summary>
        /// <param name="inputString">String de entrada</param>
        public async Task ProcessString(string inputString) =>
            await _g.Item2.ExecAsync(inputString);
    }

    /// <summary>
    /// Fachada para todas as características de negócio
    /// </summary>
    /// <remarks>
    /// Aqui chamamos de serviço de negócio (BusinessService) mas pode ser qualquer coisa que
    /// você preferir: BusinessModule, Service, Module, Facade, etc.
    /// </remarks>
    public class TudoJuntoBusinessService
    {
        private readonly LazyTuple<GenerateRandomNumberHandler, GenerateRandomPasswordHandler,
            ProcessStringHandler, ExecAllHandler> _g;

        public TudoJuntoBusinessService(
            LazyTuple<GenerateRandomNumberHandler, GenerateRandomPasswordHandler,
                ProcessStringHandler, ExecAllHandler> tuple)
        {
            Checker.NotNullArgument(tuple, nameof(tuple));

            _g = tuple;
        }

        /// <summary>
        /// Gera um número aleatório
        /// </summary>
        public async Task<int> GenerateRandomNumber() => await _g.Item1.ExecAsync();

        /// <summary>
        /// Gera uma senha aleatório com base em uma tupla que informa os caracteres possíveis e o tamanho da senha pretendida
        /// </summary>
        /// <param name="input">Tupla (<see cref="Tuple{T1,T2}"/>) de string com os caracteres possíveis, e int com o tamanho da senha pretendida</param>
        public async Task<string> GenerateRandomPassword((string, int) input) =>
            await _g.Item2.ExecAsync(input);

        /// <summary>
        /// Processa dados aleatórios com base em uma string de entrada
        /// </summary>
        /// <param name="inputString">String de entrada</param>
        public async Task ProcessString(string inputString) =>
            await _g.Item3.ExecAsync(inputString);

        /// <summary>
        /// Executa todas as outras funcionalidades para exemplificar uma característica que não
        /// precisa de parâmetros de entrada, e nem produz resultado de saída.
        /// </summary>
        public async Task ExecAll() => await _g.Item4.ExecAsync();
    }

    public enum MyNotifyType
    {
        Type1,

        [MetaTag(CustomIdKey, "NTFY-002")] Type2
    }

    public class MyRuleForNotify : RuleFor<NotificationMessage<MyNotifyType>>
    {
        public MyRuleForNotify() : base(nameof(MyNotifyType.Type1), "Confere mensagem Type1")
        {
        }

        public override Task<RuleCheckResult>
            CheckAsync(NotificationMessage<MyNotifyType> target) => Task.Run(() =>
        {
            if (!(target.Body is string))
                return RuleCheckResult.Fail;

            return RuleCheckResult.Success;
        });
    }

    public class MyRuleForNotify2 : RuleFor<NotificationMessage<MyNotifyType>>
    {
        public MyRuleForNotify2() : base("NTFY-002", "Confere mensagem Type2")
        {
        }

        public override Task<RuleCheckResult>
            CheckAsync(NotificationMessage<MyNotifyType> target) => Task.Run(() =>
        {
            if (!(target.Body is float))
                return RuleCheckResult.Fail;

            if ((float)target.Body >= 7.5)
                return new RuleCheckResult(false, new Dictionary<string, string>
                {
                    { "Numero", "O número era maior que 7.5" }
                });

            return RuleCheckResult.Success;
        });
    }

    public class MyDispatcher : INotificationDispatcher<MyNotifyType>
    {
        public Task DispatchAsync(NotificationMessage<MyNotifyType> message)
        {
            return Task.CompletedTask;
        }
    }

    public class Program
    {
        private readonly DadosEntradaBusinessService _entradaService;
        private readonly DadosSaidaBusinessService _saidaService;
        private readonly TudoJuntoBusinessService _tudoService;
        private readonly NotificationManager<MyNotifyType> _notificator;

        public Program(DadosEntradaBusinessService entradaService,
            DadosSaidaBusinessService saidaService, TudoJuntoBusinessService tudoService,
            NotificationManager<MyNotifyType> notificator)
        {
            Checker.NotNullArgument(entradaService, nameof(entradaService));
            Checker.NotNullArgument(saidaService, nameof(saidaService));
            Checker.NotNullArgument(tudoService, nameof(tudoService));
            Checker.NotNullArgument(notificator, nameof(notificator));

            _entradaService = entradaService;
            _saidaService = saidaService;
            _tudoService = tudoService;
            _notificator = notificator;
        }

        private async Task Run()
        {
            var senha = await _entradaService.GenerateRandomPassword(("12345ABCabc", 12));
            Console.WriteLine($"Senha aleatória gerada: {senha}");

            var numero = await _saidaService.GenerateRandomNumber();
            Console.WriteLine($"Número aleatório gerado: {numero}");

            await _tudoService.ExecAll();

            // Notificando mensagens sem falha
            await _notificator.NotifyAsync(MyNotifyType.Type1, "Mensagem 1");
            await _notificator.NotifyAsync(MyNotifyType.Type2, 6.0f);

            // Notificando uma mensagem com falha
            await _notificator.NotifyAsync(MyNotifyType.Type2, 7.51f);
        }

        private static async Task Main()
        {
            var services = ConfigureServices(new ServiceCollection());

            using var scope = services.BuildServiceProvider().CreateScope();
            await scope.ServiceProvider.GetService<Program>().Run();
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services.AddInfrastructure(new ConfigurationBuilder().Build())
                .AddBusiness()
                .AddScoped<Program>()
                .AddScoped<DadosEntradaBusinessService>()
                .AddScoped<DadosSaidaBusinessService>()
                .AddScoped<TudoJuntoBusinessService>();
        }
    }
}
