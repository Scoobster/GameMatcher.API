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
        private readonly PlayerDataService dataService = new PlayerDataService();

        [Route("{playerId}")]
        [HttpGet]
        public PlayerDto GetPlayer(int playerId)
        {
            try
            {
                Player player = dataService.GetPlayer(playerId);
                return PlayerDto.MapPlayerToPlayerDto(player);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [Route("{playerId}")]
        [HttpPost]
        public PlayerDto GetPlayer(int playerId, PlayerDto playerDetails)
        {
            try
            {
                Player player = dataService.UpdatePlayer(playerId, PlayerDto.MapPlayerDtoToPlayer(playerDetails));
                return PlayerDto.MapPlayerToPlayerDto(player);
            }
            catch (Exception e)
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
                return dataService.AddPlayer(PlayerDto.MapPlayerDtoToPlayer(player));
            } catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [Route("token/{playerId}")]
        [HttpPost]
        public string UpdateDeviceToken(int playerId, [FromBody] string token)
        {
            try
            {
                return dataService.UpdateDeviceToken(playerId, token);
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [Route("match/{playerId}")]
        [HttpGet]
        public MatchListDto GetAllMatches(int playerId)
        {
            try
            {
                Player player = dataService.GetPlayerWithMatches(playerId);

                List<MatchConfirmedDto> matchesConfirmed = player.ConfirmedMatches
                    .Select(m => MatchConfirmedDto.MapToDto(m))
                    .OrderBy(m => m.MatchStartTime)
                    .ToList();

                List<MatchRequestDto> matchRequests = player.MatchRequests
                    .Select(m => MatchRequestDto.MapMatchRequestToMatchRequestDto(m))
                    .OrderBy(m => m.MatchStartTime)
                    .ToList();

                return new MatchListDto
                {
                    MatchesConfirmed = matchesConfirmed,
                    MatchRequests = matchRequests
                };
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [Route("request/{playerId}")]
        [HttpGet]
        public List<MatchRequestDto> GetAvailableMatchRequests(int playerId)
        {
            List<MatchRequest> matchRequests = dataService.GetMatchRequestsAvailableForPlayer(playerId);
            return matchRequests
                .Select(request => MatchRequestDto.MapMatchRequestToMatchRequestDto(request))
                .ToList();
        }

        private HttpResponseException HandleException(Exception e)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            res.Content = new StringContent(JsonConvert.SerializeObject(e));
            return new HttpResponseException(res);
        }
    }
}
