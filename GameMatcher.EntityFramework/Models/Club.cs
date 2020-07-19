using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sport { get; set; }
        public int NumOfCourts { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
