// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using E5R.Architecture.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TransformerSample
{
    public class A
    {
        public string AMessage { get; set; }
    }

    public class B
    {
        public string BMessage { get; set; }
    }

    public class C
    {
        public string CMessage { get; set; }
    }

    public class TransformerFrom
    {
        public int ValueInteger { get; set; }
        public decimal ValueDecimal { get; set; }
        public string ValueString { get; set; }
        public A ValueClass { get; set; }
        public Guid ValueGuid { get; set; }
    }
    
    public class AutoTransformerFrom
    {
        public int ValueInteger { get; set; }
        public decimal ValueDecimal { get; set; }
        public string ValueString { get; set; }
        public A ValueClass { get; set; }
        public Guid ValueGuid { get; set; }
    }

    public class AutoTransformerTo
    {
        public int ValueInteger { get; set; }
        public int ValueIntegerNot { get; set; }
        public decimal ValueDecimal { get; set; }
        public decimal ValueDecimalNot { get; set; }
        public string ValueString { get; set; }
        public string ValueStringNot { get; set; }
        public A ValueClass { get; set; }
        public A ValueClassNot { get; set; }
        public Guid ValueGuid { get; set; }
        public Guid ValueGuidNot { get; set; }
    }

    class Program
    {
        public Program(ITransformationManager transformer)
        {
            var a = new A
            {
                AMessage = "Original message from A"
            };

            var b = new B
            {
                BMessage = "Original message from B"
            };

            var c = new C
            {
                CMessage = "Original message from C"
            };

            var listOfA = new List<A>
            {
                new A { AMessage = "Message A1" },
                new A { AMessage = "Message A2" },
                new A { AMessage = "Message A3" },
            };

            var paginatedListOfA = new PaginatedResult<A>(new List<A>
            {
                new A { AMessage = "Paginated A1" },
                new A { AMessage = "Paginated A2" },
                new A { AMessage = "Paginated A3" },
            }, 10, 3, 1000);

            var aFromB = transformer.Transform<B, A>(b);
            var aFromC = transformer.Transform<C, A>(c);
            var bFromA = transformer.Transform<A, B>(a);
            var bFromC = transformer.Transform<C, B>(c);
            var cFromA = transformer.Transform<A, C>(a);
            var cFromB = transformer.Transform<B, C>(b);
            var listOfB = transformer.Transform<A, B>(listOfA);
            var paginatedListOfB = transformer.Transform<A, B>(paginatedListOfA);
            var autoTransformerA = transformer.AutoTransform<TransformerFrom, AutoTransformerTo>(
                new TransformerFrom
                {
                    ValueInteger = 1,
                    ValueDecimal = 1.2m,
                    ValueString = "String qualquer",
                    ValueGuid = Guid.NewGuid(),
                    ValueClass = new A { AMessage = "A message from inner class" }
                });
            var autoTransformerB = transformer.AutoTransform<AutoTransformerFrom, AutoTransformerTo>(
                new AutoTransformerFrom
                {
                    ValueInteger = 2,
                    ValueDecimal = 2.3m,
                    ValueString = "String que pode ser copiada",
                    ValueGuid = Guid.NewGuid(),
                    ValueClass = new A { AMessage = "A message from inner class" }
                });

            Debug.Assert(aFromB.AMessage.Equals(b.BMessage), "Invalid aFromB");
            Debug.Assert(aFromC.AMessage.Equals(c.CMessage), "Invalid aFromC");
            Debug.Assert(bFromA.BMessage.Equals(a.AMessage), "Invalid bFromA");
            Debug.Assert(bFromC.BMessage.Equals(c.CMessage), "Invalid bFromC");
            Debug.Assert(cFromA.CMessage.Equals(a.AMessage), "Invalid cFromA");
            Debug.Assert(cFromB.CMessage.Equals(b.BMessage), "Invalid cFromB");
            Debug.Assert(listOfB != null, "List of B is null");
            Debug.Assert(listOfB.Count() == listOfA.Count, "Invalid count of list B");
            Debug.Assert(paginatedListOfB.CurrentPage == paginatedListOfA.CurrentPage,
                "Invalid CurrentPage of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Limit == paginatedListOfA.Limit,
                "Invalid Limit of paginatedListOfB");
            Debug.Assert(paginatedListOfB.NextPage == paginatedListOfA.NextPage,
                "Invalid NextPage of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Offset == paginatedListOfA.Offset,
                "Invalid Offset of paginatedListOfB");
            Debug.Assert(paginatedListOfB.PageCount == paginatedListOfA.PageCount,
                "Invalid OfPageCountfset of paginatedListOfB");
            Debug.Assert(paginatedListOfB.PreviousPage == paginatedListOfA.PreviousPage,
                "Invalid PreviousPage of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Total == paginatedListOfA.Total,
                "Invalid Total of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Result == null, "Invalid Result of paginatedListOfB");

            foreach (var (item, index) in listOfA.Select((item, index) => (item, index)))
            {
                Debug.Assert(item.AMessage.Equals(listOfB.ElementAt(index).BMessage),
                    "Invalid item pair value");
            }

            foreach (var (item, index) in paginatedListOfA.Result.Select((item, index) =>
                (item, index)))
            {
                Debug.Assert(
                    item.AMessage.Equals(paginatedListOfB.Result.ElementAt(index).BMessage),
                    "Invalid paginated item pair value");
            }
        }

        static void Main(string[] args)
        {
            var services = ConfigureServices(new ServiceCollection());

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                scope.ServiceProvider.GetService<Program>();
            }
        }

        static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Program>();

            return services.AddInfrastructure(new ConfigurationBuilder().Build());
        }
    }

    public class BFromATransformer : ITransformer<A, B>
    {
        public B Transform(A @from)
        {
            return new B
            {
                BMessage = @from.AMessage
            };
        }
    }

    public class AFromBTransformer : ITransformer<B, A>
    {
        public A Transform(B @from)
        {
            return new A
            {
                AMessage = @from.BMessage
            };
        }
    }

    public class MultTransformer : ITransformer<A, C>, ITransformer<C, A>, ITransformer<B, C>,
        ITransformer<C, B>
    {
        C ITransformer<A, C>.Transform(A @from)
        {
            return new C
            {
                CMessage = @from.AMessage
            };
        }

        A ITransformer<C, A>.Transform(C @from)
        {
            return new A
            {
                AMessage = @from.CMessage
            };
        }

        C ITransformer<B, C>.Transform(B @from)
        {
            return new C
            {
                CMessage = @from.BMessage
            };
        }

        B ITransformer<C, B>.Transform(C @from)
        {
            return new B
            {
                BMessage = @from.CMessage
            };
        }
    }

    public class TransformerWithAuthTransformer : ITransformer<TransformerFrom, AutoTransformerTo>
    {
        public AutoTransformerTo Transform(TransformerFrom @from)
        {
            if (from == null)
            {
                return null;
            }
            
            return new AutoTransformerTo
            {
                ValueClassNot = from.ValueClass,
                ValueDecimalNot = from.ValueDecimal,
                ValueIntegerNot = from.ValueInteger,
                ValueGuidNot = from.ValueGuid,
                ValueStringNot = from.ValueString
            };
        }
    }
}
