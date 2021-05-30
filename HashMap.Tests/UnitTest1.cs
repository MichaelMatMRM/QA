using System;
using Xunit;
using HashTableShared;

namespace HashMap.Tests {
    public class UnitTest1 {
        
        [Fact]
        public void TestAdd() {
            var map = new HashTable();

            map["AS09AS"] = "VITYA_CARSHARING";
            map["AS25AS"] = "MISHA";
            map["AS77AS"] = "ASS777";

            Assert.Equal("VITYA_CARSHARING", map["AS09AS"]);

            Assert.Equal("MISHA", map["AS25AS"]);

            Assert.Equal("ASS777", map["AS77AS"]);
        }


        [Theory]
        [InlineData("AS15AS", "Misha")]
        [InlineData("DF45AS", "Misha")]
        [InlineData("DF45FS", "Misha")]
        [InlineData("DF40AS", "Misha")]
        [InlineData("AA45AS", "Misha")]
        [InlineData("DF00AS", "Misha")]
        [InlineData("DF45GG", "Misha")]
        public void TestMult(string key, string value) {
            var map = new HashTable {[key] = value};
            Assert.Equal(value, map[key]);
        }

        [Fact]
        public void TestCollisions() {
            var map = new HashTable();

            map["AA04ND"] = "VITYA_CARSHARING";
            map["AA04RJ"] = "MISHA";
            map["AA07AG"] = "ASS777";

            Assert.Equal("VITYA_CARSHARING", map["AA04ND"]);
            Assert.Equal("MISHA", map["AA04RJ"]);
            Assert.Equal("ASS777", map["AA07AG"]);
        }
    }
}
