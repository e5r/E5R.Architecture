using System;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    public class DataLimiterTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new DataLimiter();

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier_With_Properties()
        {
            // Act
            var instance = new DataLimiter
            {
                OffsetBegin = 1,
                OffsetEnd = 2,
                OffsetCount = 3
            };

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(1, instance.OffsetBegin);
            Assert.Equal(2, instance.OffsetEnd);
            Assert.Equal(3, instance.OffsetCount);
        }

        [Fact]
        public void Must_Instantiate_For_Model_With_BuiltInType_Identifier()
        {
            /**
             * .NET Framework Type:
             * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/built-in-types-table
             *
             * Except: object and string
             */
            PerformTest_BuiltInType_Identifier<bool>();
            PerformTest_BuiltInType_Identifier<byte>();
            PerformTest_BuiltInType_Identifier<sbyte>();
            PerformTest_BuiltInType_Identifier<char>();
            PerformTest_BuiltInType_Identifier<decimal>();
            PerformTest_BuiltInType_Identifier<double>();
            PerformTest_BuiltInType_Identifier<float>();
            PerformTest_BuiltInType_Identifier<int>();
            PerformTest_BuiltInType_Identifier<uint>();
            PerformTest_BuiltInType_Identifier<long>();
            PerformTest_BuiltInType_Identifier<ulong>();
            PerformTest_BuiltInType_Identifier<short>();
            PerformTest_BuiltInType_Identifier<ushort>();
        }

        [Fact]
        public void Must_Instantiate_For_Model_With_BuiltInType_Identifier_With_Properties()
        {
            /**
             * .NET Framework Type:
             * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/built-in-types-table
             *
             * Except: object and string
             */
            PerformTest_BuiltInType_Identifier_With_Properties<bool>();
            PerformTest_BuiltInType_Identifier_With_Properties<byte>();
            PerformTest_BuiltInType_Identifier_With_Properties<sbyte>();
            PerformTest_BuiltInType_Identifier_With_Properties<char>();
            PerformTest_BuiltInType_Identifier_With_Properties<decimal>();
            PerformTest_BuiltInType_Identifier_With_Properties<double>();
            PerformTest_BuiltInType_Identifier_With_Properties<float>();
            PerformTest_BuiltInType_Identifier_With_Properties<int>();
            PerformTest_BuiltInType_Identifier_With_Properties<uint>();
            PerformTest_BuiltInType_Identifier_With_Properties<long>();
            PerformTest_BuiltInType_Identifier_With_Properties<ulong>();
            PerformTest_BuiltInType_Identifier_With_Properties<short>();
            PerformTest_BuiltInType_Identifier_With_Properties<ushort>();
        }

        private static void PerformTest_BuiltInType_Identifier<T>()
            where T : struct
        {
            // Act
            var instance = new DataLimiter<DataModel<T>, T>();

            // Assert
            Assert.NotNull(instance);
        }

        private static void PerformTest_BuiltInType_Identifier_With_Properties<T>()
            where T : struct
        {
            // Act
            var instance = new DataLimiter<DataModel<T>, T>
            {
                OffsetBegin = 1,
                OffsetEnd = 2,
                OffsetCount = 3
            };

            // Assert
            Assert.NotNull(instance);
            Assert.Equal(1, instance.OffsetBegin);
            Assert.Equal(2, instance.OffsetEnd);
            Assert.Equal(3, instance.OffsetCount);
        }
    }
}