using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lab6_1.Filter
{
    public class AuthFilter: Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute
    {
         public  void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    {
            var count = actionExecutedContext.Request.Properties["Count"];
           // IList ts = (IList)count;
            IEnumerable<string> enumerable = (IEnumerable<string>)count;
            actionExecutedContext.Response.Content.Headers.Add("totalHeader", enumerable);
    }

        public System.Net.Http.HttpResponseMessage httpResponse()
        {
            var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
            IEnumerable<string> m_oEnum = new List<string>() { "1", "2", "3" };
            response.Headers.Add("Finally", m_oEnum);
            return response;
        }
    }
}
