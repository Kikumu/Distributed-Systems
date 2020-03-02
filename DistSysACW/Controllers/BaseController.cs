using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
 //-----------------------------------searches database for user(TASK 3 DONE)---------------------------------------------------//
        public string search_username(string name)
        {
            string _bool = "";
            var _names = _context.Users;
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
            if (name == "")
                rslt = "Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json";
            var _names = _context.Users;
            foreach (Models.User user in _names)
            {
                string str = name;
                if (str == user.user_name)
                    rslt = "Oops. This username is in use. Please try again with a new username";
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
                       // api_key = api_val
                    };
                    _context.Users.Add(user);
                    _context.logs.Add(logs);
                    _context.SaveChanges();
                }
                rslt = "Saved";
            }
            return rslt; 
        }
        public BaseController(Models.UserContext context)
        {
           
           _context = context;
            try{
                //Random rnd = new Random();
                //int log_val = rnd.Next(1, 1000);
               
                //using (_context = new Models.UserContext())
                //{
                //    Models.Log logs = new Models.Log()
                //    {
                //        //LogID = (randomly generated but keep checking against db if theres a duplicate)
                //        Log_string = "First Signup to system", //depending on what user did generate str
                //        LogDateTime = new DateTime(2010,2,2)
                //    };
                //    //for adding.....
                //    Models.User user = new Models.User()
                //    {
                //       // user_name = post_user_temp, //grap from post func
                //        log_data = logs,
                //        api_key = 2
                //    };
                //    _context.Users.Add(user);
                //    _context.logs.Add(logs);
                    _context.SaveChanges();
                //}
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}