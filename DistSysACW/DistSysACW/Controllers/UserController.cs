using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistSysACW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        public static string post_user_temp = "";

        public UserController(Models.UserContext context) : base(context)
        {

        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet]
        [ActionName("User")]
        public string New([FromQuery]string name) //gets name value from get request and checks database if user exists
        {
            name = Convert.ToString(search_username(name));
            //convert to JSON string so that client can accept
            return name;
        }

        // POST: api/User
        [HttpPost]
        [ActionName("User")]
        public void Post([FromBody] string value)
        {
            //need to "clean" value due to parenthesis and stuff in the JSON string
            //assuming its "cleaned"
            post_user_temp = value;

        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
