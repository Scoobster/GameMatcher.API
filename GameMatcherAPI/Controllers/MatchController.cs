using GameMatcher.EntityFramework.Models;
using GameMatcher.EntityFramework.Services;
using GameMatcherAPI.Models;
using GameMatcherAPI.Services;
using Newtonsoft.Json;
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
        private readonly MatchDataService matchDataService = new MatchDataService();
        private readonly PlayerDataService playerDataService = new PlayerDataService();
        private readonly FirebaseNotificationService notificationService = new FirebaseNotificationService();

        [HttpPut]
        [Route("request")]
        public int PutMatchRequest(MatchRequestDto requestDto)
        {
            try
            {
                // Save request into db
                MatchRequest request = matchDataService.AddMatchRequest(MatchRequestDto.MapMatchRequestDtoToMatchRequest(requestDto));

                // Send match notifications to all players from same club
                List<string> allPlayerTokensFromSameClub = playerDataService.GetPlayersFromClub(request.HostPlayer.ClubId)
                    .Where(p => p.Id != request.HostPlayerId && p.DeviceToken != null)
                    .Select(p => p.DeviceToken)
                    .ToList();
                notificationService.SendRequestNotifications(request, allPlayerTokensFromSameClub);

                return request.Id;
            }
            catch (Exception e)
            {
                throw HandleException(e);
            }
        }

        [HttpPost]
        [Route("accept/{requestId}")]
        public int AcceptMatchRequest(int requestId, [FromBody] int playerId)
        {
            try
            {
                // Save request into db
                MatchConfirmed match = matchDataService.ConfirmMatch(requestId, playerId);

                // Send match confirmation notification to host player player
                string hostPlayerToken = match.HostPlayer.DeviceToken;
                notificationService.SendConfirmationOfMatchNotification(match, hostPlayerToken);

                return match.Id;
            }
            catch (Exception e)
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
