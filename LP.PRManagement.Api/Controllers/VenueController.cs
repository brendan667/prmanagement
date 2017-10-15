using System.Collections.Generic;
using System.Web.Http;

namespace LP.PRManagement.Api.Controllers
{
    public class VenueController : ApiController
    {
        // GET: api/Venue
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Venue/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Venue
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Venue/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Venue/5
        public void Delete(int id)
        {
        }
    }
}
