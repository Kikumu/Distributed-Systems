using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace DistSysACW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProtectedController : BaseController
    {
        public ProtectedController(Models.UserContext context) : base(context)
        {

        }

        // GET: api/Protected/5
        [HttpGet]
        [ActionName("hello")]
        [Authorize(Roles = "Admin,user")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        [ActionName("sha1")]
        [Authorize(Roles = "Admin,user")]
        public string security1(int id)
        {
            return "value";
        }
        [HttpGet]
        [ActionName("sha26")]
        [Authorize(Roles = "Admin,user")]
        public string security2(int id)
        {
            return "value";
        }


    }
}
