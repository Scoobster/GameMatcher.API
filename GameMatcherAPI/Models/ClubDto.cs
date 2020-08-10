using GameMatcher.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMatcherAPI.Models
{
    public class ClubDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Sport { get; set; }
        public short NumOfCourts { get; set; }

        public static Club MapPlayerDtoToPlayer(ClubDto clubDto)
        {
            Club club = new Club
            {
                Name = clubDto.Name,
                Sport = clubDto.Sport,
                NumOfCourts = clubDto.NumOfCourts
            };
            if (clubDto.Id.HasValue) club.Id = clubDto.Id.Value;
            return club;
        }

        public static ClubDto MapPlayerToPlayerDto(Club club)
        {
            return new ClubDto
            {
                Id = club.Id,
                Name = club.Name,
                Sport = club.Sport,
                NumOfCourts = club.NumOfCourts
            };
        }
    }
}