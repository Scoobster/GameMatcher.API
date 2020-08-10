using GameMatcher.EntityFramework.Models;
using GameMatcher.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Services
{
    public class MatchDataService
    {
        private readonly GameMatcherDataContext dbContext;

        public MatchDataService()
        {
            dbContext = new GameMatcherDataContext();
        }

        public MatchRequest AddMatchRequest(MatchRequest request)
        {
            request.Created = DateTime.Now;
            dbContext.MatchRequests.Add(request);
            dbContext.SaveChanges();

            return GetMatchRequest(request.Id);
        }

        public MatchRequest GetMatchRequest(int requestId)
        {
            return dbContext.MatchRequests
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == requestId);
        }

        public MatchConfirmed GetConfirmedMatch(int matchId)
        {
            return dbContext.ConfirmedMatches
                .AsNoTracking()
                .Include("HostPlayer")
                .Include("GuestPlayer")
                .FirstOrDefault(m => m.Id == matchId);
        }

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

        public MatchConfirmed ConfirmMatch(int requestId, int guestPlayerId)
        {
            var guestPlayer = dbContext.Players
                .FirstOrDefault(p => p.Id == guestPlayerId);
            var request = dbContext.MatchRequests
                .Include("HostPlayer")
                .FirstOrDefault(m => m.Id == requestId);

            MatchConfirmed match = new MatchConfirmed
            {
                Created = DateTime.Now,
                HostPlayer = request.HostPlayer,
                MatchStartTime = request.MatchStartTime,
                LengthInMins = request.LengthInMins,
                GuestPlayer = guestPlayer
            };

            dbContext.MatchRequests.Remove(request);
            dbContext.ConfirmedMatches.Add(match);

            dbContext.SaveChanges();

            return GetConfirmedMatch(match.Id);
        }
    }
}
