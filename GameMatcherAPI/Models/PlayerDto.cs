using GameMatcher.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMatcherAPI.Models
{
    public class PlayerDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClubId { get; set; }
        public short Ability { get; set; }
        public string PhoneNumber { get; set; }

        public static Player MapPlayerDtoToPlayer(PlayerDto playerDto)
        {
            Player player = new Player
            {
                FirstName = playerDto.FirstName,
                LastName = playerDto.LastName,
                ClubId = playerDto.ClubId,
                Ability = playerDto.Ability,
                PhoneNumber = playerDto.PhoneNumber,
            };
            if (playerDto.Id.HasValue) player.Id = playerDto.Id.Value;
            return player;
        }

        public static PlayerDto MapPlayerToPlayerDto(Player player)
        {
            return new PlayerDto
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                ClubId = player.ClubId,
                Ability = player.Ability,
                PhoneNumber = player.PhoneNumber,
            };
        }
    }
}