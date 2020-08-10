using GameMatcher.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMatcherAPI.Models
{
    public class MatchConfirmedDto
    {
        public int Id { get; set; }
        public int HostPlayerId { get; set; }
        public int GuestPlayerId { get; set; }
        public int? HostPlayerAbility { get; set; }
        public int? GuestPlayerAbility { get; set; }
        public DateTime Created { get; set; }
        public DateTime MatchStartTime { get; set; }
        public int LengthInMins { get; set; }

        public static MatchConfirmedDto MapToDto(MatchConfirmed match)
        {
            MatchConfirmedDto matchDto = new MatchConfirmedDto
            {
                Id = match.Id,
                HostPlayerId = match.HostPlayerId,
                GuestPlayerId = match.GuestPlayerId,
                MatchStartTime = match.MatchStartTime.Value,
                LengthInMins = match.LengthInMins,
                Created = match.Created.Value
            };
            if (match.HostPlayer != null) matchDto.HostPlayerAbility = match.HostPlayer.Ability;
            if (match.GuestPlayer != null) matchDto.GuestPlayerAbility = match.GuestPlayer.Ability;
            return matchDto;
        }
    }
}