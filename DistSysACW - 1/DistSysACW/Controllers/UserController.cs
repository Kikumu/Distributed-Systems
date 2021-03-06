﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading;

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
        public string New([FromQuery]string username) //gets name value from get request and checks database if user exists
        {
            username = Convert.ToString(search_username(username));
            obtain_keys();
            //convert to JSON string so that client can accept
            return username;
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
            if ((temp == "Empty")|| (temp == "False - User Does Not Exist! Did you mean to do a POST to create a new user?"))
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
        [Authorize(Roles = "Admin")]         //Works! Only admin can do this
        //---------------------------------- IMPLIMENTED(Change role)----------------------------------------------------------------------------------//
        public ActionResult change_role([FromBody]string test)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string name1 = identity.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            string temp_ = "";
            try {
                DistSysACWClient.Class.ObjectJsonSerialiser roleObj = JsonConvert.DeserializeObject<DistSysACWClient.Class.ObjectJsonSerialiser>(test);
                string name = roleObj.name;
                string role = roleObj.role;

                if (role == "Admin")
                {
                    temp_ = update_role(name, role);
                    if (temp_ == "Unavailable")
                    {
                        this.Response.StatusCode = 400;
                        temp_ = "NOT DONE: Username does not exist";
                        update_log(name1, "Role Update Failed");
                    }
                    else
                    {
                        this.Response.StatusCode = 200;
                        update_log(name1, "Role Update Succesfull. "+name1+", changed "+name+"'s role to "+role);
                    }
                }
                else if (role == "user")
                {
                    temp_ = update_role(name, role);
                    if (temp_ == "Unavailable")
                    {
                        this.Response.StatusCode = 400;
                        temp_ = "NOT DONE: Username does not exist";
                        update_log(name1, "Role Update Failed");
                    }
                    else
                    {
                        this.Response.StatusCode = 200;
                        update_log(name1, "Role Update Succesfull. " + name1 + ", changed " + name + "'s role to " + role);
                    }
                        
                }
                else
                {
                    this.Response.StatusCode = 400;
                    temp_ = "NOT DONE: Role does not exist";
                    update_log(name1, "Role Update Failed");
                }
            }
            catch {
                this.Response.StatusCode = 400;
                temp_ = "NOT DONE: An error occured";
                update_log(name1, "Role Update Failed");
            }
            return new ObjectResult(temp_);
        }
        
        [HttpDelete]
        [ActionName("removeuser")]
        [Authorize(Roles = "Admin")]
        //------------------------------- IMPLEMENTED(delete user(LOL its supposed to be a delete request))-----------------------------------------//
        public ActionResult delete_user_data([FromQuery]string username)
        {
            string temp = username;
            temp = delete_user(username);
            if (temp == "deleted")
                temp = "True";
            else
                temp = "False";

            return new ObjectResult(temp);
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
