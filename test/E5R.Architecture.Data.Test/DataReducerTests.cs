using System;
using Xunit;

namespace E5R.Architecture.Data.Test
{
    public class DataReducerTests
    {
        [Fact]
        public void Must_Instantiate_For_Model_Without_Identifier()
        {
            // Act
            var instance = new DataReducer();

            // Assert
            Assert.NotNull(instance);
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

        private static void PerformTest_BuiltInType_Identifier<T>()
            where T : struct
        {
            // Act
            var instance = new DataReducer<DataModel<T>, T>();

            // Assert
            Assert.NotNull(instance);
        }
    }
}