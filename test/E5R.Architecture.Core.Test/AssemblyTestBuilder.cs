// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace E5R.Architecture.Core.Test
{
    public class AssemblyTestBuilder
    {
        private readonly AssemblyBuilder _assemblyBuilder;

        public AssemblyTestBuilder(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(name), AssemblyBuilderAccess.Run);
        }

        public AssemblyTestBuilder SetCustomAttribute<TAttribute>(string attrValue)
        {
            Type type = typeof(TAttribute);
            Type[] parameters = { typeof(string) };
            object[] args = { attrValue };

            ConstructorInfo ctor = type.GetConstructor(parameters);
            CustomAttributeBuilder attrBuilder = new CustomAttributeBuilder(ctor, args);

            _assemblyBuilder.SetCustomAttribute(attrBuilder);

            return this;
        }

        public Assembly Build()
        {
            return _assemblyBuilder.GetModules()
                .FirstOrDefault()
                .Assembly;
        }
    }
}
