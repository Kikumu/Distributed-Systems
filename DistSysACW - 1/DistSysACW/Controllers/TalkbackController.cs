using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistSysACW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TalkBackController : BaseController
    {
        /// <summary>
        /// Constructs a TalkBack controller, taking the UserContext through dependency injection
        /// </summary>
        /// <param name="context">DbContext set as a service in Startup.cs and dependency injected</param>
        public TalkBackController(Models.UserContext context) : base(context) { }

        [HttpGet(Name = "hello")]
        [ActionName("hello")]

        //------------------------------------------------TASK1 DONE-----------------------------------------------------------------//
        public ActionResult hello()
        {
            return new ObjectResult("Hello world");
        }
        //-----------------------------------------------TASK2 DONE-----------------------------------------------------------------//
        [HttpGet]
        public ActionResult Sort([FromQuery]int[]num)
        {
            // bool anyname = num.All(char.IsDigit);
            //check array for strings
            try {
                Array.Sort(num);
                ObjectResult result = new ObjectResult(num);
                return result;
            }
            catch
            {
                this.Response.StatusCode = 400;
                return new ObjectResult("BAD REQUEST");
            }
        }
        //----------------------------------------------TASK3(Basically scans your database and checks if user is available)------------------------//

        //public IActionResult Get([FromQuery]int[] integers)
        //{
        //    #region TASK1
        //    // TODO: 
        //    // sort the integers into ascending order
        //    // send the integers back as the api/talkback/sort response
        //    #endregion
        //}
    }
}
