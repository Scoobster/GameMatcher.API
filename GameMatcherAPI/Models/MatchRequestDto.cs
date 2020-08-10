using GameMatcher.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMatcherAPI.Models
{
    public class MatchRequestDto
    {
        public int? Id { get; set; }
        public int HostPlayerId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? MatchStartTime { get; set; }
        public int LengthInMins { get; set; }
        public short? MinAbility { get; set; }
        public short? MaxAbility { get; set; }
        public short? HostPlayerAbility { get; set; }

        public static MatchRequest MapMatchRequestDtoToMatchRequest(MatchRequestDto requestDto)
        {
            MatchRequest request = new MatchRequest
            {
                HostPlayerId = requestDto.HostPlayerId,
                MatchStartTime = requestDto.MatchStartTime,
                LengthInMins = requestDto.LengthInMins,
            };
            if (requestDto.Id.HasValue) request.Id = requestDto.Id.Value;
            if (requestDto.Created.HasValue) request.Created = requestDto.Created.Value;
            if (requestDto.MinAbility.HasValue) request.MinAbility = requestDto.MinAbility.Value;
            if (requestDto.MaxAbility.HasValue) request.MaxAbility = requestDto.MaxAbility.Value;
            return request;
        }

        public static MatchRequestDto MapMatchRequestToMatchRequestDto(MatchRequest request)
        {
            MatchRequestDto requestDto = new MatchRequestDto
            {
                Id = request.Id,
                HostPlayerId = request.HostPlayerId,
                MatchStartTime = request.MatchStartTime,
                LengthInMins = request.LengthInMins,
                Created = request.Created
            };
            if (request.MinAbility.HasValue) requestDto.MinAbility = request.MinAbility.Value;
            if (request.MaxAbility.HasValue) requestDto.MaxAbility = request.MaxAbility.Value;
            if (request.HostPlayer != null) requestDto.HostPlayerAbility = request.HostPlayer.Ability;
            return requestDto;
        }
    }
}