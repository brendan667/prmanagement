﻿using System.Collections.Generic;
using System.Web.Http;

namespace LP.PRManagement.Api.Controllers
{
    public class FestivalController : ApiController
    {
        // GET: api/Festival
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Festival/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Festival
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Festival/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Festival/5
        public void Delete(int id)
        {
        }
    }
}
