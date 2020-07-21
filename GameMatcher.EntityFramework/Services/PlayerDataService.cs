using GameMatcher.EntityFramework.Models;
using GameMatcher.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Services
{
    public class PlayerDataService
    {
        private readonly GameMatcherDataContext dbContext;

        public PlayerDataService()
        {
            dbContext = new GameMatcherDataContext();
        }

        public Player GetPlayer(int playerId)
        {
            return dbContext.Players
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == playerId);
        }

        public int AddPlayer(Player player)
        {
            dbContext.Players.Add(player);
            dbContext.SaveChanges();
            return player.Id;
        }

        public string UpdateDeviceToken(int playerId, string token)
        {
            Player player = dbContext.Players.FirstOrDefault(p => p.Id == playerId);
            player.DeviceToken = token;
            dbContext.SaveChanges();
            return player.DeviceToken;
        }

        //public MatchRequest GetMatchRequest(int requestId)
        //{
        //    return dbContext.MatchRequests
        //        .AsNoTracking()
        //        .FirstOrDefault(m => m.Id == requestId);
        //}

        //public MatchConfirmed GetMatch(int matchId)
        //{
        //    return dbContext.ConfirmedMatches
        //        .AsNoTracking()
        //        .FirstOrDefault(m => m.Id == matchId);
        //}

        //public List<MatchRequest> GetMatchRequests(DateTime startTime, DateTime endTime)
        //{
        //    return dbContext.MatchRequests
        //        .AsNoTracking()
        //        .Where(m => (m.MatchStartTime >= startTime) && (m.MatchStartTime <= endTime))
        //        .ToList();
        //}

        //public List<MatchRequest> GetMatchRequestsForPlayer(int playerId)
        //{
        //    return dbContext.MatchRequests
        //        .AsNoTracking()
        //        .Where(m => m.HostPlayerId == playerId)
        //        .ToList();
        //}

        //public List<MatchConfirmed> GetMatchesForPlayer(int playerId)
        //{
        //    return dbContext.ConfirmedMatches
        //        .AsNoTracking()
        //        .Where(m => m.HostPlayerId == playerId || m.GuestPlayerId == playerId)
        //        .ToList();
        //}

        //public int AddClub(Club club)
        //{
        //    dbContext.Clubs.Add(club);
        //    dbContext.SaveChanges();
        //    return club.Id;
        //}

        //public int AddMatchRequest(MatchRequest request)
        //{
        //    dbContext.MatchRequests.Add(request);
        //    dbContext.SaveChanges();
        //    return request.Id;
        //}

        //public List<Player> GetListOfPlayersForRequestSending(int requestId)
        //{
        //    MatchRequest request = dbContext.MatchRequests
        //        .FirstOrDefault(m => m.Id == requestId);
        //    return dbContext.Players
        //        .AsNoTracking().
        //        Where(p => p.ClubId == request.HostPlayer.ClubId &&
        //            p.Ability <= request.HostPlayer.Ability + 1 && p.Ability >= request.HostPlayer.Ability - 1)
        //        .ToList();
        //}

        //public int ConfirmMatch(int requestId, int guestPlayerId)
        //{
        //    var guestPlayer = dbContext.Players.FirstOrDefault(p => p.Id == guestPlayerId);
        //    var request = dbContext.MatchRequests.FirstOrDefault(m => m.Id == requestId);

        //    MatchConfirmed match = new MatchConfirmed
        //    {
        //        HostPlayer = request.HostPlayer,
        //        MatchStartTime = request.MatchStartTime,
        //        LengthInMins = request.LengthInMins,
        //        GuestPlayer = guestPlayer
        //    };

        //    dbContext.MatchRequests.Remove(request);
        //    dbContext.ConfirmedMatches.Add(match);

        //    dbContext.SaveChanges();

        //    return match.Id;
        //}
    }
}
