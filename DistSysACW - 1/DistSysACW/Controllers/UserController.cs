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
        //--------------------------------------------------------------------------------SEARCH DB FOR NAME--------------------------------------------//
        [HttpGet]
        [ActionName("new")]
        public string New([FromQuery]string name) //gets name value from get request and checks database if user exists
        {
            name = Convert.ToString(search_username(name));
            obtain_keys();
            //convert to JSON string so that client can accept
            return name;
        }

        // POST: api/User
        //------------------------------------------------------------ADD NEW USR------------------------------------------------------------------//
        [HttpPost]
        [ActionName("new")]
        public ActionResult Post([FromBody] string value)
        {
            //string temp_1 = value;
            string temp = "";
            temp = Convert.ToString(add_user(value));
            //need to "clean" value due to parenthesis and stuff in the JSON string
            //assuming its "cleaned"
            //post_user_temp = value;
            if (temp == "Empty")
            {
                this.Response.StatusCode = 400;
                temp = ("Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json");
               
            }
            else if(temp == "Taken")
            {
                this.Response.StatusCode = 403;
                temp = ("Oops. This username is already in use. Please try again with a new username.");
            }
            obtain_keys();
            return new ObjectResult(temp);

        }
        [HttpPost]
        [ActionName("ChangeRole")]
        //----------------------------------PARTIALLY IMPLIMENTED(Change role)----------------------------------------------------------------------------------//
        public string change_role([FromQuery]string name,[FromHeader]int id)
        {
            string temp_ = "";
            temp_ = update_role_to_admin(name);
            return temp_;
        }
        [HttpDelete]
        [ActionName("DeleteUser")]
        //-------------------------------PARTIALLY IMPLEMENTED(delete user(LOL its supposed to be a delete request))-----------------------------------------//
        public string delete_user_data([FromQuery]string name,[FromHeader]int id)
        {
            string temp = "";
            temp = delete_user(name);
            return temp;
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
