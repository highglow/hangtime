using System.Collections.Generic;
using System.Web.Http;
using Hangfire;

namespace Vega.Controllers
{
//    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            var p = new Person{Name = "Sally Fitzgibbons"};
            BackgroundJob.Enqueue<IPersonActor>(x => x.SayHello(p));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
