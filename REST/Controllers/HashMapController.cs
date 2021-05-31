using Microsoft.AspNetCore.Mvc;
using HashTableShared;

namespace REST.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HashMapController : ControllerBase {
    
        [HttpGet("{request}")]
        public HashTable Get(string request) {
            var table = new HashTable();

            foreach (var pair in request.Split('=')) {
                var data = pair.Split('-');
                if (data.Length != 2) continue;
                table[data[0]] = data[1];
            }
            
            return table;
        }

    }
}
