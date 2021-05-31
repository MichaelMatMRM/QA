using Xunit;

using HashTableShared;
using REST.Controllers;

namespace HashMap.Tests {
    public class RestTests {

        [Theory]
        [InlineData("AA99AA-hello=SS34SD-HI")]
        public void TestControllerGet(string request) {
            var table = new HashTable();

            foreach (var pair in request.Split('=')) {
                var data = pair.Split('-');
                if (data.Length != 2) continue;
                table[data[0]] = data[1];
            }

            var controller = new HashMapController();
            var map = controller.Get(request);


            Assert.Equal(table.Elements, map.Elements);
        }

    }
}
