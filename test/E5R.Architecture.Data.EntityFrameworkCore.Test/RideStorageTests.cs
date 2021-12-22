// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Core;
using E5R.Architecture.Data.EntityFrameworkCore.Alias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace E5R.Architecture.Data.EntityFrameworkCore.Test
{
    public class RideStorageTests
    {
        [Fact]
        public void Requires_ValidDbContextAndQueryableObject()
        {
            var query = new List<EmptyDataModel>().AsQueryable();
            var dbContext = new DbContext(new DbContextOptionsBuilder().Options);

            // RideStorage
            {
                var exception1 = Assert.Throws<ArgumentNullException>(
                    () => new RideStorage<EmptyDataModel>(null, query)
                );

                var exception2 = Assert.Throws<ArgumentNullException>(
                    () => new RideStorage<EmptyDataModel>(dbContext, null)
                );

                Assert.Equal("dbContext", exception1.ParamName);
                Assert.Equal("query", exception2.ParamName);
            }

            // Alias: RideStore
            {
                var exception1 = Assert.Throws<ArgumentNullException>(
                    () => new RideStore<EmptyDataModel>(null, query)
                );

                var exception2 = Assert.Throws<ArgumentNullException>(
                    () => new RideStore<EmptyDataModel>(dbContext, null)
                );

                Assert.Equal("dbContext", exception1.ParamName);
                Assert.Equal("query", exception2.ParamName);
            }

            // Alias: RideRepository
            {
                var exception1 = Assert.Throws<ArgumentNullException>(
                    () => new RideRepository<EmptyDataModel>(null, query)
                );

                var exception2 = Assert.Throws<ArgumentNullException>(
                   () => new RideRepository<EmptyDataModel>(dbContext, null)
               );

                Assert.Equal("dbContext", exception1.ParamName);
                Assert.Equal("query", exception2.ParamName);
            }
        }
    }

    #region Mock
    public class EmptyDataModel : IIdentifiable
    {
        public object[] Identifiers => throw new NotImplementedException();
    }
    #endregion
}
