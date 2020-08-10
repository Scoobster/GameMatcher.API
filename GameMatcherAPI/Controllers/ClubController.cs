using GameMatcher.EntityFramework.Services;
using GameMatcherAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameMatcherAPI.Controllers
{
    [RoutePrefix("api/club")]
    public class ClubController : ApiController
    {
        private readonly ClubDataService clubDataService = new ClubDataService();

        [Route("")]
        [HttpGet]
        public List<InputOption> GetAllClubs()
        {
            return clubDataService.GetClubs()
                .Select(c => new InputOption { Label = c.Name, Value = c.Id.ToString() })
                .OrderBy(c => c.Label)
                .ToList();
        }
    }
}
