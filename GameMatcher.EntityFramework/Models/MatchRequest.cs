using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Models
{
    public class MatchRequest
    {
        public int Id { get; set; }
        public int HostPlayerId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime MatchStartTime { get; set; }
        public int LengthInMins { get; set; }
        public short? MinAbility { get; set; }
        public short? MaxAbility { get; set; }

        public virtual Player HostPlayer { get; set; }
    }
}
