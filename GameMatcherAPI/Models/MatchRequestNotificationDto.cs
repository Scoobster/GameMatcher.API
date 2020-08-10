using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMatcherAPI.Models
{
    public class MatchRequestNotificationDto
    {
        public int Id { get; set; }
        public int HostPlayerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime MatchStartTime { get; set; }
        public int LengthInMins { get; set; }
        public short HostPlayerAbility { get; set; }
    }
}