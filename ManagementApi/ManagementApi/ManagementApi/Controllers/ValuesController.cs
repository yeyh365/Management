using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManagementApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post(string value)
        {
            var a = value;
            return $"这是就是{a}";
        }

        // PUT api/values/5
        public string Put(int id, [FromBody] string value)
        {
            return value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
