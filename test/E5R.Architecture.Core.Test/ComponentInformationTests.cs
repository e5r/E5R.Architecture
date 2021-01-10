// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

namespace E5R.Architecture.Core.Test
{
    public class ComponentInformationTests
    {
        [Fact]
        public void Must_Identify_All_Assembly_Metadata()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var semver = new SemVer
            {
                Major = 1,
                Minor = 2,
                Patch = 3
            };

            var assembly = new AssemblyTestBuilder("TesteAssembly1")
                .SetCustomAttribute<AssemblyTitleAttribute>("TesteAssembly1 Title")
                .SetCustomAttribute<AssemblyDescriptionAttribute>("Meu Assembly de Teste")
                .SetCustomAttribute<AssemblyCompanyAttribute>("Assembly Ltda.")
                .SetCustomAttribute<AssemblyProductAttribute>("Meu produto no Assembly")
                .SetCustomAttribute<AssemblyCopyrightAttribute>("Copyright (c) 2020")
                .SetCustomAttribute<AssemblyTrademarkAttribute>("E5R Development Team")
                .SetCustomAttribute<GuidAttribute>(guid.ToString())
                .SetCustomAttribute<AssemblyVersionAttribute>("1.2.3")
                .Build();

            // Act
            var info = ComponentInformation.MakeFromAssembly(assembly);

            // Assert
            Assert.NotNull(info);
            Assert.NotNull(info.Version);
            Assert.Equal("TesteAssembly1", info.Name);
            Assert.Equal("TesteAssembly1 Title", info.Title);
            Assert.Equal("Meu Assembly de Teste", info.Description);
            Assert.Equal("Assembly Ltda.", info.Company);
            Assert.Equal("Meu produto no Assembly", info.Product);
            Assert.Equal("Copyright (c) 2020", info.Copyright);
            Assert.Equal("E5R Development Team", info.Trademark);
            Assert.Equal(guid.ToString(), info.Guid);
            Assert.Equal(semver.Major, info.Version.Major);
            Assert.Equal(semver.Minor, info.Version.Minor);
            Assert.Equal(semver.Patch, info.Version.Patch);
        }
    }
}
