// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using Xunit;
using E5R.Architecture.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using E5R.Architecture.Core;
using E5R.Architecture.Data.EntityFrameworkCore.Alias;

namespace E5R.Architecture.Data.EntityFrameworkCore.Test
{
    public class RideStorageTests
    {
        [Fact]
        public void Requires_ValidQueryableObject()
        {
            // RideStorage
            {
                var exception = Assert.Throws<ArgumentNullException>(
                    () => new RideStorage<EmptyDataModel>(null)
                );

                Assert.Equal("query", exception.ParamName);
            }

            // Alias: RideStore
            {
                var exception = Assert.Throws<ArgumentNullException>(
                    () => new RideStore<EmptyDataModel>(null)
                );

                Assert.Equal("query", exception.ParamName);
            }

            // Alias: RideRepository
            {
                var exception = Assert.Throws<ArgumentNullException>(
                    () => new RideRepository<EmptyDataModel>(null)
                );

                Assert.Equal("query", exception.ParamName);
            }
        }

        [Fact]
        public void TheFindMethodCannotBeImplemented()
        {
            var emptyQuery = new List<EmptyDataModel>().AsQueryable();

            // RideStorage
            {
                // Arrange
                var expectedErrorMessage = $"{typeof(RideStorage<EmptyDataModel>).Name} not implement Find(";

                // Act
                var exception1 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideStorage<EmptyDataModel>(emptyQuery);

                    ride.Find(new object());
                });

                var exception2 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideStorage<EmptyDataModel>(emptyQuery);

                    ride.Find(new object[] { });
                });

                var exception3 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideStorage<EmptyDataModel>(emptyQuery);

                    ride.Find(new EmptyDataModel());
                });

                // Assert
                Assert.StartsWith(expectedErrorMessage, exception1.Message);
                Assert.StartsWith(expectedErrorMessage, exception2.Message);
                Assert.StartsWith(expectedErrorMessage, exception3.Message);
            }

            // Alias: RideStore
            {
                // Arrange
                var expectedErrorMessage = $"{typeof(RideStore<EmptyDataModel>).Name} not implement Find(";

                // Act
                var exception1 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideStore<EmptyDataModel>(emptyQuery);

                    ride.Find(new object());
                });

                var exception2 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideStore<EmptyDataModel>(emptyQuery);

                    ride.Find(new object[] { });
                });

                var exception3 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideStore<EmptyDataModel>(emptyQuery);

                    ride.Find(new EmptyDataModel());
                });

                // Assert
                Assert.StartsWith(expectedErrorMessage, exception1.Message);
                Assert.StartsWith(expectedErrorMessage, exception2.Message);
                Assert.StartsWith(expectedErrorMessage, exception3.Message);
            }

            // Alias: RideRepository
            {
                // Arrange
                var expectedErrorMessage = $"{typeof(RideRepository<EmptyDataModel>).Name} not implement Find(";

                // Act
                var exception1 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideRepository<EmptyDataModel>(emptyQuery);

                    ride.Find(new object());
                });

                var exception2 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideRepository<EmptyDataModel>(emptyQuery);

                    ride.Find(new object[] { });
                });

                var exception3 = Assert.Throws<DataLayerException>(() =>
                {
                    var ride = new RideRepository<EmptyDataModel>(emptyQuery);

                    ride.Find(new EmptyDataModel());
                });

                // Assert
                Assert.StartsWith(expectedErrorMessage, exception1.Message);
                Assert.StartsWith(expectedErrorMessage, exception2.Message);
                Assert.StartsWith(expectedErrorMessage, exception3.Message);
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
