using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClubId { get; set; }
        public short Ability { get; set; }
        public string PhoneNumber { get; set; }
        public string DeviceToken { get; set; }

        public virtual Club Club { get; set; }
        public virtual ICollection<MatchRequest> MatchRequests { get; set; }
        public virtual ICollection<MatchConfirmed> ConfirmedMatchesAsHost { get; set; }
        public virtual ICollection<MatchConfirmed> ConfirmedMatchesAsGuest { get; set; }

        public List<MatchConfirmed> ConfirmedMatches
        {
            get
            {
                var matches = ConfirmedMatchesAsHost != null ? new List<MatchConfirmed>(ConfirmedMatchesAsHost) : new List<MatchConfirmed>();
                if (ConfirmedMatchesAsGuest != null) matches.AddRange(ConfirmedMatchesAsGuest);
                return matches;
            }
        }
    }
}
