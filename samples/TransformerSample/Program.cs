// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using E5R.Architecture.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TransformerSample
{
    public enum TransformerVariant
    {
        Variant1,
        Variant2,
        Variant3
    }
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
            var cFromAv1 = transformer.Transform<A, C, TransformerVariant>(a, TransformerVariant.Variant1);
            var cFromAv2 = transformer.Transform<A, C, TransformerVariant>(a, TransformerVariant.Variant2);
            var cFromAv3 = transformer.Transform<A, C, TransformerVariant>(a, TransformerVariant.Variant3);
            var cFromB = transformer.Transform<B, C>(b);
            var listOfB = transformer.Transform<A, B>(listOfA);
            var paginatedListOfB = transformer.Transform<A, B>(paginatedListOfA);

            Debug.Assert(aFromB.AMessage.Equals(b.BMessage), "Invalid aFromB");
            Debug.Assert(aFromC.AMessage.Equals(c.CMessage), "Invalid aFromC");
            Debug.Assert(bFromA.BMessage.Equals(a.AMessage), "Invalid bFromA");
            Debug.Assert(bFromC.BMessage.Equals(c.CMessage), "Invalid bFromC");
            Debug.Assert(cFromA.CMessage.Equals(a.AMessage), "Invalid cFromA");
            Debug.Assert(cFromAv1.CMessage.Equals(a.AMessage + "/Variant1"), "Invalid cFromA/Variant1");
            Debug.Assert(cFromAv2.CMessage.Equals(a.AMessage + "/Variant2"), "Invalid cFromA/Variant2");
            Debug.Assert(cFromAv3.CMessage.Equals(a.AMessage + "/Variant3"), "Invalid cFromA/Variant3");
            Debug.Assert(cFromB.CMessage.Equals(b.BMessage), "Invalid cFromB");
            Debug.Assert(listOfB != null, "List of B is null");
            Debug.Assert(listOfB.Count() == listOfA.Count, "Invalid count of list B");
            Debug.Assert(paginatedListOfB.CurrentPage == paginatedListOfA.CurrentPage, "Invalid CurrentPage of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Limit == paginatedListOfA.Limit, "Invalid Limit of paginatedListOfB");
            Debug.Assert(paginatedListOfB.NextPage == paginatedListOfA.NextPage, "Invalid NextPage of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Offset == paginatedListOfA.Offset, "Invalid Offset of paginatedListOfB");
            Debug.Assert(paginatedListOfB.PageCount == paginatedListOfA.PageCount, "Invalid OfPageCountfset of paginatedListOfB");
            Debug.Assert(paginatedListOfB.PreviousPage == paginatedListOfA.PreviousPage, "Invalid PreviousPage of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Total == paginatedListOfA.Total, "Invalid Total of paginatedListOfB");
            Debug.Assert(paginatedListOfB.Result == null, "Invalid Result of paginatedListOfB");

            foreach (var (item, index) in listOfA.Select((item, index) => (item, index)))
            {
                Debug.Assert(item.AMessage.Equals(listOfB.ElementAt(index).BMessage), "Invalid item pair value");
            }

            foreach (var (item, index) in paginatedListOfA.Result.Select((item, index) => (item, index)))
            {
                Debug.Assert(item.AMessage.Equals(paginatedListOfB.Result.ElementAt(index).BMessage), "Invalid paginated item pair value");
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
        ITransformer<C, B>, ITransformer<A, C, TransformerVariant>
    {
        C ITransformer<A, C>.Transform(A @from)
        {
            return new C
            {
                CMessage = @from.AMessage
            };
        }

        public C Transform(A @from, TransformerVariant operation)
        {
            switch (operation)
            {
                case TransformerVariant.Variant1:
                    return new C
                    {
                        CMessage = $"{@from.AMessage}/Variant1"
                    };
                case TransformerVariant.Variant2:
                    return new C
                    {
                        CMessage = $"{@from.AMessage}/Variant2"
                    };
                case TransformerVariant.Variant3:
                    return new C
                    {
                        CMessage = $"{@from.AMessage}/Variant3"
                    };
                default:
                    return new C
                    {
                        CMessage = @from.AMessage
                    };
            }
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
}
