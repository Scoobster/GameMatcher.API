using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Models
{
    public class MatchConfirmed
    {
        public int Id { get; set; }
        public int HostPlayerId { get; set; }
        public int GuestPlayerId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? MatchStartTime { get; set; }
        public int LengthInMins { get; set; }

        public virtual Player HostPlayer { get; set; }
        public virtual Player GuestPlayer { get; set; }
    }
}
