using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace DistSysACW.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Models.UserContext dbContext)
        {
            //Checks if api-key from header is valid-----------------------------------------------------------------//

            #region Task5
            // TODO:  Find if a header ‘ApiKey’ exists, and if it does, check the database to determine if the given API Key is valid
            //        Then set the correct roles for the User, using claims
            #endregion

            string test = "";
            test = context.Request.Headers["apikey"].ToString();   //grabs specified header from request header
            if (test != "")
            {
                int _key = int.Parse(test);
                //Grab db model
                Models.UserContext userContext = new Models.UserContext();
                var _role = userContext.Users;
                string operator_ = "";             //username
                string designation_ = "";          //role
                foreach (Models.User user in _role)
                {
                    if (_key == user.api_key)
                    {
                        //obtain relevant user and role
                        operator_ = user.user_name;
                        designation_ = user.role;

                    }
                }
                // ClaimTypes.Name = operator_;
                if(operator_ != "" && designation_ != "")
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, operator_));
                    claims.Add(new Claim(ClaimTypes.Role, designation_));
                    claims.Add(new Claim(ClaimTypes.Authentication, context.Request.Headers["apikey"]));

                    var identity = new ClaimsIdentity(claims);
                }
              
            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

    }
}
