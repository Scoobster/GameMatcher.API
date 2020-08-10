using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMatcherAPI.Models
{
    public class MatchListDto
    {
        public List<MatchRequestDto> MatchRequests { get; set; }
        public List<MatchConfirmedDto> MatchesConfirmed { get; set; }
    }
}