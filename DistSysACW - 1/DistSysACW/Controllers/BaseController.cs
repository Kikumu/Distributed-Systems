﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistSysACW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistSysACW.Controllers
{
    
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        //private UserController fooManager = new UserController();
        protected readonly Models.UserContext _context;
        //store admin api-key and user api-key?
        //---------------------------------------------api key storage in client for authentication----------------------------------------------------------------------------------------//
        public List<int> admin_api = new List<int>();  //STORE ADMIN API KEY to client 
        public List<int> user_api = new List<int>();   //STORE USER API KEY  to client 
        public UserContext context;

        protected BaseController(UserContext context)
        {
            this.context = context;
        }
        //--------------------------------------------------APIKEYS STORED TO CLIENT.NEED TO PLUG INTO HEADER--------------------------------------------//
        public void obtain_keys()
        {
            var _keys = context.Users;
           
            foreach (Models.User user in _keys)
            {
                //I just wanna obtain admin and user api key
                if ("Admin" == user.role)
                    admin_api.Add (user.api_key);
                else if ("user" == user.role)
                    user_api.Add(user.api_key);
            }
        }
        //-----------------------------------searches database for user(TASK 3 DONE)---------------------------------------------------//
        public string search_username(string name)
        {
            string _bool = "";
            var _names = context.Users;
            foreach(Models.User user in _names)
            {
                string str = name;
                if (str == user.user_name)
                    _bool = "True - User Does Exist! Did you mean to do a POST to create a new user?";
                else
                    _bool = "False - User Does Not Exist! Did you mean to do a POST to create a new user?";
            }
            if (_bool == "")
            {
                _bool = "False - User Does Not Exist! Did you mean to do a POST to create a new user?";
            }
           
            return _bool;
        }
//-------------------------------------TASK 4 ADD USERNAME TO DATABASE--------------------------------------------//

        public string add_user(string name)
        {
            string rslt = "";
            if (name == "" || name == null)
                rslt = "Empty";
            var _names = context.Users;
            foreach (Models.User user in _names)
            {
                string str = name;
                if (str == user.user_name)
                    rslt = "Taken";
            }
            Random rnd = new Random();
            int log_val = rnd.Next(1234, 5000);
            int api_val = rnd.Next(1234, 5000);

            if (rslt == "")
            {
                using (Models.UserContext _context = new Models.UserContext())
                {
                    Models.Log logs = new Models.Log()
                    {
                        //LogID = log_val,
                        Log_string = "First Signup to system", //depending on what user did generate str
                        LogDateTime = new DateTime(2010, 2, 2)
                    };
                    //for adding.....
                    Models.User user = new Models.User()
                    {
                        user_name = name, //grap from post func
                        log_data = logs,
                        role = "user"
                        
                       // api_key = api_val
                    };
                    rslt = Convert.ToString(user.api_key);
                    _context.Users.Add(user);
                    _context.logs.Add(logs);
                    _context.SaveChanges();
                }
               // rslt = "Saved";
            }
            return rslt; 
        }

        //------------------------------------UPDATING ROLE FUNCTION. WORKS BUT PARTIALLY IMPLIMENTED------------------------------------//
        public string update_role_to_admin(string name)
        {
            string rslt = "";
            var _names = context.Users;
            foreach (Models.User user in _names)
            {
                string str = name;
                if (str == user.user_name)
                    rslt = "Taken";
            }

            if (rslt != "")
            {
                var _cntxt = new Models.UserContext();
                var t_name = _cntxt.Users.Where(u => u.user_name == name).First();
                t_name.role = "Admin";
                _cntxt.SaveChanges();
                rslt = "Changed";
            }
            else
            {
                rslt = "Unavailable";
            }
            return rslt;
           
        }

        public string update_role_to_user(string name)
        {
            var _cntxt = new Models.UserContext();
            var t_name = _cntxt.Users.Where(u => u.user_name == name).First();
            t_name.role = "User";
            _cntxt.SaveChanges();
            return "Changed";
        }



        //----------------------------------DELETE ROLE FUNCTION. WORKS BUT PARTIALLY IMPLEMENTED------------------------------------------------------------------//
        public string delete_user(string name)
        {
            // db.Database.Log = Console.WriteLine;
            var _cntxt = new Models.UserContext();
            var t_name = _cntxt.Users.Where(u => u.user_name == name).First();
            _cntxt.Users.Remove(t_name);
            _cntxt.SaveChanges();
            return "deleted";
        }

        //public BaseController(Models.UserContext context)
        //{
           
        //   _context = context;
        //    try{
        //        //Random rnd = new Random();
        //        //int log_val = rnd.Next(1, 1000);
               
        //        //using (_context = new Models.UserContext())
        //        //{
        //        //    Models.Log logs = new Models.Log()
        //        //    {
        //        //        //LogID = (randomly generated but keep checking against db if theres a duplicate)
        //        //        Log_string = "First Signup to system", //depending on what user did generate str
        //        //        LogDateTime = new DateTime(2010,2,2)
        //        //    };
        //        //    //for adding.....
        //        //    Models.User user = new Models.User()
        //        //    {
        //        //       // user_name = post_user_temp, //grap from post func
        //        //        log_data = logs,
        //        //        api_key = 2
        //        //    };
        //        //    _context.Users.Add(user);
        //        //    _context.logs.Add(logs);
        //            _context.SaveChanges();
        //        //}
        //    }
        //    catch(Exception ex)
        //    {

        //    }
            
        //}
    }
}