using System;

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
