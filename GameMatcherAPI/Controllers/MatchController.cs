using GameMatcher.EntityFramework.Models;
using GameMatcher.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameMatcherAPI.Controllers
{
    [RoutePrefix("api/match")]
    public class MatchController : ApiController
    {
        private readonly GameMatcherDataService DataService = new GameMatcherDataService();

        [HttpGet]
        [Route("player/{id}")]
        public List<MatchConfirmed> GetConfirmedMatches(int playerId)
        {
            return DataService.GetMatchesForPlayer(playerId);
        }
    }
}
