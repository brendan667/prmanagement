using AutoMapper;
using LP.PRManagement.Common;
using LP.PRManagement.Common.Models.DTOs.User;
using LP.PRManagement.Core.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace LP.PRManagement.Api.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        private readonly IConfig _config;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(IUserManager userManager, IConfig config)
        {
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// Registration method
        /// </summary>
        /// <param name="model">User Model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]UserModel model)
        {
            return Ok("test");
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserResponseModel>> Get()
        {
            return (await _userManager.GetAll()).Select(x => Mapper.Map<UserResponseModel>(x));
        }

        /// <summary>
        /// Getting a specific user
        /// </summary>
        /// <param name="refId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<UserResponseModel> Get(Guid id)
        {
            return Mapper.Map<UserResponseModel>(await _userManager.Get(id));
        }

        /// <summary>
        /// Gets all users paged
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<UserResponseModel>> GetPaged(int? page, int? count)
        {
            var takePage = page ?? 1;
            var takeCount = count ?? 20;

            return (await _userManager.GetAll())
                            .Skip((takePage - 1) * takeCount)
                            .Take(takeCount).Select(x => Mapper.Map<UserResponseModel>(x));
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

        [HttpGet]
        [Route("api/v1/TestCall")]
        public IHttpActionResult TestCall()
        {
            return Ok("test call");
        }
    }
}
