// Copyright (c) E5R Development Team. All rights reserved.
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
    /// Característica que gera um número aleatório
    /// </summary>
    public class GenerateRandomNumberFeature : OutputOnlyBusinessFeature<int>
    {
        protected override async Task<int> ExecActionAsync() =>
            await Task.Run(() => new Random().Next(int.MinValue, int.MaxValue));
    }

    /// <summary>
    /// Característica que processa uma string de entrada
    /// </summary>
    public class ProcessStringFeature : InputOnlyBusinessFeature<string>
    {
        protected override async Task ExecActionAsync(string input)
            => await Task.Run(() =>
            {
                // Aqui é garantido que "input" não é nulo

                if (string.IsNullOrEmpty(input))
                {
                    input = "abc";
                }

                var builder = new StringBuilder();

                input.ToList().ForEach(c =>
                {
                    var bytes = new byte[(int) c];

                    new Random().NextBytes(bytes);

                    foreach (var b in bytes)
                    {
                        builder.Append(b.ToString("x"));
                    }
                });

                var stringGerada = builder.ToString();
                var tamanhoString = stringGerada.Length;
            });
    }

    /// <summary>
    /// Característica que gera uma senha aleatória com base em uma lista de caracteres válidos
    /// </summary>
    public class GenerateRandomPasswordFeature : BusinessFeature<(string, int), string>
    {
        protected override async Task<string> ExecActionAsync((string, int) input) =>
            await Task.Run(() =>
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

                return builder.ToString();
            });
    }

    /// <summary>
    /// Característica que executa todas as outras 3 features
    /// </summary>
    public class ExecAllFeature : BusinessFeature
    {
        private readonly GenerateRandomNumberFeature _generateRandomNumberFeature;
        private readonly GenerateRandomPasswordFeature _generateRandomPasswordFeature;
        private readonly ProcessStringFeature _processStringFeature;

        public ExecAllFeature(GenerateRandomNumberFeature generateRandomNumberFeature,
            GenerateRandomPasswordFeature generateRandomPasswordFeature,
            ProcessStringFeature processStringFeature)
        {
            Checker.NotNullArgument(generateRandomNumberFeature,
                nameof(generateRandomNumberFeature));
            Checker.NotNullArgument(generateRandomPasswordFeature,
                nameof(generateRandomPasswordFeature));
            Checker.NotNullArgument(processStringFeature, nameof(processStringFeature));

            _generateRandomNumberFeature = generateRandomNumberFeature;
            _generateRandomPasswordFeature = generateRandomPasswordFeature;
            _processStringFeature = processStringFeature;
        }

        protected override async Task ExecActionAsync()
        {
            var number = await _generateRandomNumberFeature.ExecAsync();
            var password = await _generateRandomPasswordFeature.ExecAsync((
                "012345 6789abcdefgh    ijklmnopqrs tuvwxyz-)(*&ˆˆ$#@!", 10));

            await _processStringFeature.ExecAsync("nova string");
        }
    }

    /// <summary>
    /// Fachada para funcionalidades de negócio que aceitam parâmetros de entrada
    /// </summary>
    /// <remarks>
    /// Aqui chamamos de módulo de negócio (BusinessModule) mas pode ser qualquer coisa que
    /// você preferir: Service, BusinessService, Module, Facade, etc.
    /// </remarks>
    public class DadosEntradaBusinessModule : LazyGroup<
        ProcessStringFeature,
        GenerateRandomPasswordFeature>
    {
        public DadosEntradaBusinessModule(ILazy<ProcessStringFeature> item1,
            ILazy<GenerateRandomPasswordFeature> item2) : base(item1, item2)
        {
        }

        /// <summary>
        /// Processa dados aleatórios com base em uma string de entrada
        /// </summary>
        /// <param name="inputString">String de entrada</param>
        public async Task ProcessString(string inputString) =>
            await Item1.ExecAsync(inputString);

        /// <summary>
        /// Gera uma senha aleatório com base em uma tupla que informa os caracteres possíveis e o tamanho da senha pretendida
        /// </summary>
        /// <param name="input">Tupla (<see cref="Tuple{T1,T2}"/>) de string com os caracteres possíveis, e int com o tamanho da senha pretendida</param>
        public async Task<string> GenerateRandomPassword((string, int) input) =>
            await Item2.ExecAsync(input);
    }

    /// <summary>
    /// Fachada para características de negócio que produzem resultado de saída
    /// </summary>
    /// <remarks>
    /// Aqui chamamos de módulo de negócio (BusinessModule) mas pode ser qualquer coisa que
    /// você preferir: Service, BusinessService, Module, Facade, etc.
    /// </remarks>
    public class
        DadosSaidaBusinessModule : LazyGroup<GenerateRandomNumberFeature, ProcessStringFeature>
    {
        public DadosSaidaBusinessModule(ILazy<GenerateRandomNumberFeature> item1,
            ILazy<ProcessStringFeature> item2) : base(item1, item2)
        {
        }

        /// <summary>
        /// Gera um número aleatório
        /// </summary>
        public async Task<int> GenerateRandomNumber() => await Item1.ExecAsync();

        /// <summary>
        /// Processa dados aleatórios com base em uma string de entrada
        /// </summary>
        /// <param name="inputString">String de entrada</param>
        public async Task ProcessString(string inputString) =>
            await Item2.ExecAsync(inputString);
    }

    /// <summary>
    /// Fachada para todas as características de negócio
    /// </summary>
    /// <remarks>
    /// Aqui chamamos de módulo de negócio (BusinessModule) mas pode ser qualquer coisa que
    /// você preferir: Service, BusinessService, Module, Facade, etc.
    /// </remarks>
    public class TudoJuntoBusinessModule : LazyGroup<GenerateRandomNumberFeature,
        GenerateRandomPasswordFeature, ProcessStringFeature, ExecAllFeature>
    {
        public TudoJuntoBusinessModule(ILazy<GenerateRandomNumberFeature> item1,
            ILazy<GenerateRandomPasswordFeature> item2, ILazy<ProcessStringFeature> item3,
            ILazy<ExecAllFeature> item4) : base(item1, item2, item3, item4)
        {
        }

        /// <summary>
        /// Gera um número aleatório
        /// </summary>
        public async Task<int> GenerateRandomNumber() => await Item1.ExecAsync();

        /// <summary>
        /// Gera uma senha aleatório com base em uma tupla que informa os caracteres possíveis e o tamanho da senha pretendida
        /// </summary>
        /// <param name="input">Tupla (<see cref="Tuple{T1,T2}"/>) de string com os caracteres possíveis, e int com o tamanho da senha pretendida</param>
        public async Task<string> GenerateRandomPassword((string, int) input) =>
            await Item2.ExecAsync(input);

        /// <summary>
        /// Processa dados aleatórios com base em uma string de entrada
        /// </summary>
        /// <param name="inputString">String de entrada</param>
        public async Task ProcessString(string inputString) =>
            await Item3.ExecAsync(inputString);

        /// <summary>
        /// Executa todas as outras funcionalidades para exemplificar uma característica que não
        /// precisa de parâmetros de entrada, e nem produz resultado de saída.
        /// </summary>
        public async Task ExecAll() => await Item4.ExecAsync();
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

            if ((float) target.Body >= 7.5)
                return new RuleCheckResult(false, new Dictionary<string, string>
                {
                    {"Numero", "O número era maior que 7.5"}
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
        private readonly DadosEntradaBusinessModule _entradaModule;
        private readonly DadosSaidaBusinessModule _saidaModule;
        private readonly TudoJuntoBusinessModule _tudoModule;
        private readonly NotificationManager<MyNotifyType> _notificator;

        public Program(DadosEntradaBusinessModule entradaModule,
            DadosSaidaBusinessModule saidaModule, TudoJuntoBusinessModule tudoModule,
            NotificationManager<MyNotifyType> notificator)
        {
            Checker.NotNullArgument(entradaModule, nameof(entradaModule));
            Checker.NotNullArgument(saidaModule, nameof(saidaModule));
            Checker.NotNullArgument(tudoModule, nameof(tudoModule));
            Checker.NotNullArgument(notificator, nameof(notificator));

            _entradaModule = entradaModule;
            _saidaModule = saidaModule;
            _tudoModule = tudoModule;
            _notificator = notificator;
        }

        private async Task Run()
        {
            var senha = await _entradaModule.GenerateRandomPassword(("12345ABCabc", 12));
            Console.WriteLine($"Senha aleatória gerada: {senha}");

            var numero = await _saidaModule.GenerateRandomNumber();
            Console.WriteLine($"Número aleatório gerado: {numero}");

            await _tudoModule.ExecAll();
            
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
            services.AddScoped<Program>();

            return services.AddInfrastructure(new ConfigurationBuilder().Build()).AddBusiness();
        }
    }
}
