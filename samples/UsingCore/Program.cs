using System;
using E5R.Architecture.Core;

namespace UsingCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = new SemVer
            {
                Major = 0,
                Minor = 1,
                Label = "alpha"
            };

            Console.WriteLine($"Hello World v{version}!");
        }
    }
}
