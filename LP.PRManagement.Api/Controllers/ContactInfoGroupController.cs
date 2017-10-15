using System.Collections.Generic;
using System.Web.Http;

namespace LP.PRManagement.Api.Controllers
{
    public class ContactInfoGroupController : ApiController
    {
        // GET: api/ContactInfoGroup
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ContactInfoGroup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ContactInfoGroup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ContactInfoGroup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ContactInfoGroup/5
        public void Delete(int id)
        {
        }
    }
}
