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
        protected readonly Models.UserContext _context;
 //-----------------------------------searches database for user(TASK 3 DONE)---------------------------------------------------//
        public bool search_username(string name)
        {
            var _bool;
            var _names = _context.Users;
            foreach(Models.User user in _names)
            {
                string str = name;
                if (str == user.user_name)
                    _bool = true;
                else
                    _bool = false;
            }
            return _bool;
        }
//-------------------------------------TASK 4 ADD USERNAME TO DATABASE--------------------------------------------//
        public BaseController(Models.UserContext context)
        {
           _context = context;
            try{
                Random rnd = new Random();
                int log_val = rnd.Next(1, 1000);

                using (_context = new Models.UserContext())
                {
                    Models.Log logs = new Models.Log()
                    {
                        //LogID = (randomly generated but keep checking against db if theres a duplicate)
                        Log_string = "", //depending on what user did generate str
                        LogDateTime = new DateTime(2010,2,2)
                    };
                    //for adding.....
                    Models.User user = new Models.User()
                    {
                        user_name = "", //grap from http?
                        log_data = logs,
                        api_key = 2
                    };
                    _context.Users.Add(user);
                    _context.logs.Add(logs);
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}