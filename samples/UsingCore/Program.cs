using E5R.Architecture.Core;

namespace UsingCore
{
    using static System.Console;

    internal static class Program
    {
        private static void Main()
        {
            var version = new SemVer
            {
                Major = 0,
                Minor = 1,
                Label = "alpha"
            };

            WriteLine($"Hello World v{version}!");
        }
    }
}
