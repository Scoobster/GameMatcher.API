using GameMatcher.EntityFramework.Models;
using GameMatcher.EntityFramework.Services;
using GameMatcherAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GameMatcherAPI.Controllers
{
    [RoutePrefix("api/player")]
    public class PlayerController : ApiController
    {
        private readonly PlayerDataService DataService = new PlayerDataService();

        [Route("{playerId}")]
        [HttpGet]
        public PlayerDto GetPlayer(int playerId)
        {
            try
            {
                Player player = DataService.GetPlayer(playerId);
                return PlayerDto.MapPlayerToPlayerDto(player);
            } catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [Route("")]
        [HttpPut]
        public int PutPlayer(PlayerDto player)
        {
            try
            {
                return DataService.AddPlayer(PlayerDto.MapPlayerDtoToPlayer(player));
            } catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [Route("token/{playerId}")]
        [HttpPost]
        public string UpdateDeviceToken(int playerId, [FromBody]string token)
        {
            try
            {
                return DataService.UpdateDeviceToken(playerId, token);
            } catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        private HttpResponseException HandleException(Exception e)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            res.Content = new StringContent(JsonConvert.SerializeObject(e));
            return new HttpResponseException(res);
        }
    }
}
