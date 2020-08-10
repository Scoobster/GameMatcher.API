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

        public Player UpdatePlayer(int playerId, Player playerDetails)
        {
            Player player = dbContext.Players
                .FirstOrDefault(p => p.Id == playerId);

            player.FirstName = playerDetails.FirstName;
            player.LastName = playerDetails.LastName;
            player.ClubId = playerDetails.ClubId;
            player.Ability = playerDetails.Ability;
            player.PhoneNumber = playerDetails.PhoneNumber;

            dbContext.SaveChanges();

            return GetPlayer(playerId);
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
            return dbContext.Players.FirstOrDefault(p => p.Id == playerId).DeviceToken;
        }

        public IQueryable<Player> GetPlayersFromClub(int clubId)
        {
            return dbContext.Players
                .AsNoTracking()
                .Where(p => p.ClubId == clubId);
        }

        public Player GetPlayerWithMatches(int playerId)
        {
            return dbContext.Players
                .AsNoTracking()
                .Include("MatchRequests")
                .Include("ConfirmedMatchesAsHost")
                .Include("ConfirmedMatchesAsGuest")
                .FirstOrDefault(p => p.Id == playerId);
        }

        public List<MatchRequest> GetMatchRequestsAvailableForPlayer(int playerId)
        {
            short playerAbility = GetPlayer(playerId).Ability;
            return dbContext.MatchRequests
                .AsNoTracking()
                .Include("HostPlayer")
                .Where(request => request.HostPlayerId != playerId)
                .Where(request => (request.MinAbility.HasValue ? request.MinAbility.Value <= playerAbility : request.HostPlayer.Ability - 1 <= playerAbility)
                    && (request.MaxAbility.HasValue ? request.MinAbility.Value >= playerAbility : request.HostPlayer.Ability + 1 >= playerAbility))
                .ToList();
        }
    }
}
