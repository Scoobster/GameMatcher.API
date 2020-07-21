﻿using GameMatcher.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameMatcherAPI.Controllers
{
    [RoutePrefix("api/controller/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("get-values")]
        public IEnumerable<string> GetValues()
        {
            var dataService = new PlayerDataService();
            //dataService.Initialise();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
