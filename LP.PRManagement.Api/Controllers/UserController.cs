using LP.PRManagement.Core.Managers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LP.PRManagement.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserController : ApiController
    {
        public IUserManager UserManager { get; }

        public UserController(IUserManager userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Registration method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]UserModel model)
        {
            return Ok("test");
        }


        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public async Task<string> Get(int id)
        {
            return await UserManager.GetUser(id);
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
