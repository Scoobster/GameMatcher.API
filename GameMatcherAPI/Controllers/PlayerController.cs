using GameMatcher.EntityFramework.Models;
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
    [RoutePrefix("api/player")]
    public class PlayerController : ApiController
    {
        private readonly GameMatcherDataService DataService = new GameMatcherDataService();

        [Route("{playerId}")]
        [HttpGet]
        public PlayerDto GetPlayer(int playerId)
        {
            Player player = DataService.GetPlayer(playerId);
            return PlayerDto.MapPlayerToPlayerDto(player);
        }

        [HttpPut]
        public int PutPlayer(PlayerDto player)
        {
            return DataService.AddPlayer(PlayerDto.MapPlayerDtoToPlayer(player));
        }
    }
}
