// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace E5R.Architecture.Core
{
    public class ComponentInformation
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Product { get; set; }
        public string Copyright { get; set; }
        public string Trademark { get; set; }
        public string Guid { get; set; }
        public SemVer Version { get; set; }

        public static ComponentInformation MakeFromAssembly(Assembly assembly)
        {
            Checker.NotNullArgument(assembly, nameof(assembly));

            var name = assembly.GetName();
            var titleAttr = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var descriptionAttr = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            var companyAttr = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            var productAttr = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            var trademarkAttr = assembly.GetCustomAttribute<AssemblyTrademarkAttribute>();
            var guidAttr = assembly.GetCustomAttribute<GuidAttribute>();
            var versionAttr = assembly.GetCustomAttribute<AssemblyVersionAttribute>();

            Version sysVersion = name.Version;

            if (versionAttr != null)
                sysVersion = new Version(versionAttr.Version);

            return new ComponentInformation
            {
                Name = name.Name,
                Title = titleAttr?.Title,
                Description = descriptionAttr?.Description,
                Company = companyAttr?.Company,
                Product = productAttr?.Product,
                Copyright = copyrightAttr?.Copyright,
                Trademark = trademarkAttr?.Trademark,
                Guid = guidAttr?.Value,
                Version = new SemVer
                {
                    Major = sysVersion.Major,
                    Minor = sysVersion.Minor,
                    Patch = sysVersion.Build
                }
            };
        }
    }
}
