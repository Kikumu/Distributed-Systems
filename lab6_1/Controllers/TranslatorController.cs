using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace lab6_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        // GET: api/Translator
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Translator/5
        [HttpGet]
        [ActionName("GetInt")]
        public  string Get([FromQuery]string name)
        {
          
            //string json_string = "{/$name/}";
            string json_string = "{/" + name + "/}";
            //name get_name = new name()
            //{
            //    data = name
            //};
            return JsonConvert.SerializeObject(json_string);
           // return json_string;
           //use client to just transform the data into readable format by server
        }
        [HttpPost]
        [ActionName("GetInt")]
        public string Get_p([FromBody]string data)   //accepts a json object(need to accept a json string)//string json done (used dixons example)
        {
            //string response = "";
            Filter.AuthFilter authFilter = new Filter.AuthFilter();
            authFilter.httpResponse();
            return data;
        }
        [HttpGet]
        [ActionName("Response")]
        public System.Net.Http.HttpResponseMessage httpResponse1()
        {
            //HttpRequest httpRequest = new HttpRequest();
           // httpRequest.Headers.
            var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
            IEnumerable<string> m_oEnum = new List<string>() { "1", "2", "3" };
            response.Headers.Add("Finally", m_oEnum);
            return response;
        }

        
       
        [HttpGet]
        public string Bin([FromQuery]string input)
        {
            return "Your string1 is " + input;
        }
        [HttpGet("{input1}", Name = "Bin1")]
        public string Bin1(string input1)
        {
            return "Your string2 is " + input1;
        }
        // POST: api/Translator
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Translator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //public async Task<BindingAddress> Get__()
        //{
        //    Request.Properties["Count"] = "123";
        //}
    }
}
