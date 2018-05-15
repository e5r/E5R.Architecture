// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

namespace E5R.Architecture.Core
{
    public class SemVer
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public string Label { get; set; }

        public override string ToString()
        {
            string label = !string.IsNullOrEmpty(Label)
                ? $"-{Label}"
                : string.Empty;

            return $"{Major}.{Minor}.{Patch}{label}";
        }
    }
}
