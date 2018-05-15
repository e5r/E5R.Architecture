// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

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
